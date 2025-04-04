using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using FaucetSharp.Core.Handlers;
using FaucetSharp.Core.interceptors;
using FaucetSharp.Core.Interceptors;
using FaucetSharp.Models.Objects.Config;
using Serilog;

namespace FaucetSharp.Core.Channels;

public abstract class AbstractChannel<TChannelConfig, TPacketInterceptor> : IChannel<TChannelConfig, TPacketInterceptor>
    where TChannelConfig : IChannelConfig
    where TPacketInterceptor : IPacketInterceptor
{
    protected UdpClient Transport { get; }
    
    public ILogger Logger { get; }
    
    public TChannelConfig Config { get; }
    public IPEndPoint EndPoint { get; }
    public bool HasAuthority { get; }

    public TPacketInterceptor PacketInterceptor { get; protected init; }
    public IPacketSerializer Serializer { get; protected init; }
    public IPacketDeserializer Deserializer { get; protected init; }
    
    public CancellationTokenSource Signal { get; }
    public CancellationToken Token { get; }
    
    public ConcurrentQueue<UdpReceiveResult> Queue { get; }
    public SemaphoreSlim Semaphore { get; protected set; }
    public List<Task> Workers { get; protected set; }

    protected AbstractChannel(IPEndPoint endPoint, TChannelConfig config , bool hasAuthority)
    {
        Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        
        Config = config;
        EndPoint = endPoint;
        HasAuthority = hasAuthority;
        
        // Setup signalling values
        Signal = new CancellationTokenSource();
        Token = Signal.Token;
        
        // Setup processing values
        Queue = new ConcurrentQueue<UdpReceiveResult>();
        Semaphore = new SemaphoreSlim(1, 1);
        Workers = Enumerable
            .Range(0, config.MaxWorkerCount)
            .Select(_ => Task.Run(Process, Token))
            .ToList();
        
        try
        {
            if (hasAuthority)
            {
                Transport = new UdpClient(EndPoint);
            }
            else
            {
                Transport = new UdpClient();
                Transport.Client.Bind(new IPEndPoint(IPAddress.Any, 0));
            }
        }
        catch (SocketException e)
        {
            Logger.Fatal($"Cannot connect to {endPoint}.", e);
            throw new Exception($"Cannot connect to {endPoint}.", e);
        }
    }

    public abstract Task Listen();

    public abstract Task Process();

    public virtual bool IsShuttingDown() => Token.IsCancellationRequested;
}