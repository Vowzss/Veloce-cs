using System.Net;

namespace veloce.shared.channels.client;

public abstract class AbstractClientChannel : AbstractChannel, IClientChannel
{
    protected AbstractClientChannel(IPEndPoint endPoint) : base(endPoint, false)
    {
    }
}