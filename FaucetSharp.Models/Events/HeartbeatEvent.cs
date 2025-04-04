using System.Net;

using FaucetSharp.Models.Events.Args;
using FaucetSharp.Models.Packets.Heartbeat;

namespace FaucetSharp.Models.Events;

public delegate void HeartbeatEvent(HeartbeatEventArgs args);

public sealed class HeartbeatEventArgs : AbstractEventPacketArgs
{
    public HeartbeatEventArgs(IPEndPoint sender, IHeartbeatPacket packet) : base(sender, packet)
    {
    }
}