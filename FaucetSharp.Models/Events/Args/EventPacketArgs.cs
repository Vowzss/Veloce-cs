using System.Net;
using FaucetSharp.Models.Packets;

namespace FaucetSharp.Models.Events.Args;

public abstract class AbstractEventPacketArgs : AbstractEventArgs, IEventPacketArgs
{
    public AbstractEventPacketArgs(IPEndPoint sender, IPacket packet) : base(sender)
    {
        Packet = packet;
    }

    public IPacket Packet { get; }
}

public sealed class EventPacketArgs : AbstractEventPacketArgs
{
    public EventPacketArgs(IPEndPoint sender, IPacket packet) : base(sender, packet)
    {
    }
}