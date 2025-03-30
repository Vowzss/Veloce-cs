using System.Net;
using veloce.shared.packets;

namespace veloce.shared.events.client;

public delegate void HeartbeatEvent(HeartbeatEventArgs args);

public sealed class HeartbeatEventArgs : AbstractEventPacketArgs
{
    public HeartbeatEventArgs(IPEndPoint sender, IHeartbeatPacket packet) : base(sender, packet)
    {
    }
}