using System.Net;
using veloce.shared.packets;

namespace veloce.shared.events;

public abstract class AbstractEventPacketArgs : AbstractEventArgs, IEventPacketArgs
{
    public IPacket Packet { get; }

    public AbstractEventPacketArgs(IPEndPoint sender, IPacket packet) : base(sender)
    {
        Packet = packet;
    }
}

public sealed class EventPacketArgs : AbstractEventPacketArgs
{
    public EventPacketArgs(IPEndPoint sender, IPacket packet) : base(sender, packet)
    {
    }
}