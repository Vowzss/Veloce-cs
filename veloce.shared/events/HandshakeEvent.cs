using System.Net;
using veloce.shared.packets;

namespace veloce.shared.events;

public delegate void HandshakeEvent(HandshakeEventArgs args);

public sealed class HandshakeEventArgs : AbstractEventPacketArgs
{
    public HandshakeEventArgs(IPEndPoint sender, IHandshakePacket packet) : base(sender, packet)
    {
    }
}