using System.Net;
using FaucetSharp.Core.interceptors.client;
using FaucetSharp.Core.Packets;
using FaucetSharp.Models.Enums;
using FaucetSharp.Models.Events.Args;
using FaucetSharp.Models.Objects.Config.Client;
using FaucetSharp.Models.Objects.Encryption.Client;
using FaucetSharp.Models.Packets;

namespace FaucetSharp.Core.Channels.Client;

public abstract class AbstractClientChannel : AbstractChannel<IClientConfig, IClientPacketInterceptor>, IClientChannel
{
    public IClientEncryption Encryption { get; }
    
    protected AbstractClientChannel(IPEndPoint endPoint, IClientConfig config,  IClientEncryption encryption) : base(endPoint, config, false)
    {
        Encryption = encryption;
    }

    public Task Connect()
    {
        Logger.Information("Connecting...");
        Task.Run(Listen, Token);
        
        return Send(new FaucetHandshakePacket(HandshakeStep.PublicKey));
    }

    public Task Disconnect()
    {
        Logger.Information("Disconnecting...");
        Signal.Cancel();
        Transport.Close();
        
        return Task.CompletedTask;
    }

    public async Task Heartbeat()
    {
        while (!IsShuttingDown())
        {
            await Send(new FaucetHeartbeatPacket(Config.PlayerId, HeartbeatStep.Ping));
            await Task.Delay(Config.HeartbeatRate, Token);
        }
    }

    public override async Task Listen()
    {
        var lastLogTime = DateTime.UtcNow;

        while (!IsShuttingDown())
        {
            if (Queue.Count >= Config.MaxProcessThreshold)
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
                Queue.Enqueue(await Transport.ReceiveAsync(Token));
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An error occured while listening from transport.");
            }

            await Task.Delay(1, Token);
        }
    }

    public override async Task Process()
    {
        while (!IsShuttingDown())
        {
            await Semaphore.WaitAsync(Token);

            try
            {
                while (Queue.TryDequeue(out var rs))
                {
                    var args = new DataReceiveArgs(rs.RemoteEndPoint, rs.Buffer);
                    PacketInterceptor.Accept(args, Encryption);
                }
            }
            finally
            {
                Semaphore.Release();
                await Task.Delay(1, Token);
            }
        }
    }

    public async Task Send(IPacket packet)
    {
        Logger.Information($"Client sending packet:[{packet.Identifier}].");
        
        try
        {
            var data = Serializer.Write(packet, Encryption);
            await Transport.SendAsync(data, data.Length, EndPoint);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, $"Failed to transport packet:[{packet.Identifier}]!");
        }
    }
}