using System.Net;
using veloce.shared.packets;

namespace veloce.shared.events;

public delegate void DisconnectEvent(DisconnectEventArgs args);

public sealed class DisconnectEventArgs : AbstractEventPacketArgs
{
    public DisconnectEventArgs(IPEndPoint sender, IDisconnectPacket packet) : base(sender, packet)
    {
    }
}