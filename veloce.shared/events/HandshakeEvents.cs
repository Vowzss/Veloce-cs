using System.Net;
using veloce.shared.packets;

namespace veloce.shared.events;

public delegate void FirstHandshakeEvent(FirstHandshakeEventArgs args);

public sealed class FirstHandshakeEventArgs : AbstractEventPacketArgs
{
    public FirstHandshakeEventArgs(IPEndPoint sender, IFirstHandshakePacket packet) : base(sender, packet)
    {
    }
}

public delegate void SecondHandshakeEvent(SecondHandshakeEventArgs args);

public sealed class SecondHandshakeEventArgs : AbstractEventPacketArgs
{
    public SecondHandshakeEventArgs(IPEndPoint sender, ISecondHandshakePacket packet) : base(sender, packet)
    {
    }
}