using System.Net;
using FaucetSharp.Shared.packets;

namespace FaucetSharp.Shared.events;

public delegate void HandshakeEvent(HandshakeEventArgs args);

public sealed class HandshakeEventArgs : AbstractEventPacketArgs
{
    public HandshakeEventArgs(IPEndPoint sender, IHandshakePacket packet) : base(sender, packet)
    {
    }
}