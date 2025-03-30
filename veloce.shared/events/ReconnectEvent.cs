using System.Net;
using veloce.shared.packets;

namespace veloce.shared.events;

public delegate void ReconnectEvent(ReconnectEventArgs args);

public sealed class ReconnectEventArgs : AbstractEventPacketArgs
{
    public ReconnectEventArgs(IPEndPoint sender, IReconnectPacket packet) : base(sender, packet)
    {
    }
}