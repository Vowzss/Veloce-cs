using System.Net;
using veloce.shared.packets;

namespace veloce.shared.events.client;

public delegate void PongEvent(PongEventArgs args);

public sealed class PongEventArgs : AbstractEventPacketArgs
{
    public PongEventArgs(IPEndPoint sender, IPongPacket packet) : base(sender, packet)
    {
    }
}

public delegate void PingEvent(PingEventArgs args);

public sealed class PingEventArgs : AbstractEventPacketArgs
{
    public PingEventArgs(IPEndPoint sender, IPingPacket packet) : base(sender, packet)
    {
    }
}