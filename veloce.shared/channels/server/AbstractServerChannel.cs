using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using veloce.shared.enums;
using veloce.shared.events;
using veloce.shared.handlers;
using veloce.shared.interceptors.server;
using veloce.shared.models;
using veloce.shared.packets;
using veloce.shared.utils;

namespace veloce.shared.channels.server;

public abstract class AbstractServerChannel : AbstractChannel<IServerPacketInterceptor>, IServerChannel
{
    public IServerConfig Config { get; }
    
    public ServerState State { get; protected set; } = ServerState.Unknown;
    public ServerStatus Status { get; protected set; } = ServerStatus.Unknown;
    public required IServerSessionHandler SessionHandler { get; init; }

    public event TickEvent OnTick;
    public event TickMissedEvent OnTickMissed;
    
    private readonly CancellationToken _token;
    private readonly ITickingClock _clock;
    
    private readonly SemaphoreSlim _semaphore;
    private readonly ConcurrentQueue<UdpReceiveResult> _queue;
    private List<Task> _workers;
    
    protected AbstractServerChannel(IPEndPoint endPoint, IServerConfig config) : base(endPoint, true)
    {
        Config = config;
        
        _token = Signal.Token;
        
        // Setup ticking clock
        _clock = new ServerTickingClock(Config.TickRate, _token);
        _clock.OnTick += () => OnTick?.Invoke();
        _clock.OnTickMissed += time => OnTickMissed?.Invoke(time);
        
        // Setup processing values
        _semaphore = new SemaphoreSlim(Config.MaxWorkerCount);
        _queue = new ConcurrentQueue<UdpReceiveResult>();
    }
    
    public virtual void Start()
    {
        Logger.Information("Starting...");
        Status = ServerStatus.Starting;
        
        Task.Run(Listen, _token);
        Task.Run(_clock.Tick, _token);
        
        Status = ServerStatus.Online;
        Logger.Information("Online!");
    }

    public virtual void Stop()
    {
        Logger.Information("Stopping...");
        Status = ServerStatus.Stopping;
        
        Signal.Cancel();
        Transport.Close();
        _semaphore.Dispose();
        _queue.Clear();
        _workers.Clear();
        
        Status = ServerStatus.Offline;
        Logger.Information("Offline!");
    }

    public async Task Listen()
    {
        _workers = Enumerable
            .Range(0, Config.MaxWorkerCount)
            .Select(_ => Task.Run(Process, _token))
            .ToList();
        
        var lastLogTime = DateTime.UtcNow;
        
        while (Status == ServerStatus.Online || !_token.IsCancellationRequested)
        {
            if (_queue.Count >= Config.ProcessingThreshold)
            {
                var currentTime = DateTime.UtcNow;
                if (currentTime - lastLogTime >= TimeSpan.FromSeconds(30))
                {
                    Logger.Warning("Too many packets are waiting to be processed.");
                    lastLogTime = currentTime;
                }
            }
            
            try
            {
                _queue.Enqueue(await Transport.ReceiveAsync(_token));
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An error occured while listening from transport.");
            }
            
            await Task.Delay(1, _token);
        }
    }
    
    public override async Task Process()
    {
        while (Status == ServerStatus.Online || !_token.IsCancellationRequested)
        {
            await _semaphore.WaitAsync(_token);
            
            try
            {
                while (_queue.TryDequeue(out var rs))
                {
                    var session = SessionHandler.FindByEndpoint(rs.RemoteEndPoint);
                    var args = new DataReceiveArgs(rs.RemoteEndPoint, rs.Buffer);
                    PacketInterceptor.Accept(args, session?.Encryption);
                }
            }
            finally
            {
                _semaphore.Release();
                await Task.Delay(1, _token);
            }
        }
    }
    
    public async Task Send(IServerSession session, IPacket packet)
    {
        Logger.Information($"Sending '{packet.Identifier}' packet to client ID:{session.Id}.");
        
        if (session.Status != ClientStatus.Connected)
        {
            Logger.Warning($"Cancelled '{packet.Identifier}' packet due to unstable session ID:{session.Id}.");
            return;
        }
        
        try
        {
            var data = Serializer.Write(packet, session.Encryption);
            await Transport.SendAsync(data, data.Length, session.EndPoint);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, $"Failed to transport '{packet.Identifier}' packet.");
        }
    }

    public async Task Broadcast(IPacket packet)
    {
        Logger.Information($"Broadcasting '{packet.Identifier}' packet to connected clients.");
        
        foreach (var session in SessionHandler.GetAll())
            await Send(session, packet);
    }
}

public sealed class VeloceServerChannel : AbstractServerChannel
{
    public VeloceServerChannel(IPEndPoint endPoint, IServerConfig config) : base(endPoint, config)
    {
    }

    public override void Start()
    {
        base.Start();
        
        PacketInterceptor.OnHandshake += async args =>
        {
            var packet = args.Packet as IHandshakePacket;
            var session = SessionHandler.FindByEndpoint(args.Sender) 
                          ?? SessionHandler.Register(args.Sender);
            
            switch (packet!.Step)
            {
                case HandshakeStep.PublicKey:
                    await Send(session, new VeloceHandshakePacket
                    {
                        Key = Config.GetPublicKey(),
                        Step = HandshakeStep.PublicKey
                    });
                    break;
                case HandshakeStep.AesKey:
                    session.Encryption.LoadAesKey(Config.Rsa, packet.Key!);
                    await Send(session, new VeloceHandshakePacket
                    {
                        Step = HandshakeStep.AesKey
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        };
        
    }
}