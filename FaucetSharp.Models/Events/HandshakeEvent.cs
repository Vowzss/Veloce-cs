using System.Net;

using FaucetSharp.Models.Events.Args;
using FaucetSharp.Models.Packets.Handshake;

namespace FaucetSharp.Models.Events;

public delegate void HandshakeEvent(HandshakeEventArgs args);

public sealed class HandshakeEventArgs : AbstractEventPacketArgs
{
    public HandshakeEventArgs(IPEndPoint sender, IHandshakePacket packet) : base(sender, packet)
    {
    }
}