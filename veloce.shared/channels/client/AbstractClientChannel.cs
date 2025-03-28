using System.Net;
using veloce.shared.interceptors.client;
using veloce.shared.models;
using veloce.shared.packets;

namespace veloce.shared.channels.client;

public abstract class AbstractClientChannel : AbstractChannel<IClientPacketInterceptor>, IClientChannel
{
    public EncryptionContext? Encryption { get; init; }

    protected AbstractClientChannel(IPEndPoint endPoint) : base(endPoint, false)
    {
    }

    public void Send(IPacket packet)
    {
        try
        {
            var data = Serializer.Write(packet, Encryption);
            Transport.Send(data, data.Length, EndPoint);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to transport packet.");
        }
    }
}

public sealed class DefaultClientChannel : AbstractClientChannel
{
    public DefaultClientChannel(IPEndPoint endPoint) : base(endPoint)
    {
    }

    public override async Task Process()
    {
        Logger.Warning("I cannot process anything for now.");
    }
}