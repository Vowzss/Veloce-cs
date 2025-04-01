using System.Net;
using veloce.shared.interceptors.client;
using veloce.shared.models;
using veloce.shared.packets;

namespace veloce.shared.channels.client;

public abstract class AbstractClientChannel : AbstractChannel<IClientPacketInterceptor>, IClientChannel
{
    public EncryptionContext? Encryption { get; }

    protected AbstractClientChannel(IPEndPoint endPoint) : base(endPoint, false)
    {
        Encryption = new EncryptionContext();
    }

    public async Task Send(IPacket packet)
    {
        try
        {
            var data = Serializer.Write(packet, Encryption);
            await Transport.SendAsync(data, data.Length, EndPoint);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to transport packet.");
        }
    }
}

public sealed class VeloceClientChannel : AbstractClientChannel
{
    public VeloceClientChannel(IPEndPoint endPoint) : base(endPoint)
    {
    }

    public override async Task Process()
    {
        Logger.Warning("I cannot process anything for now.");
    }
}