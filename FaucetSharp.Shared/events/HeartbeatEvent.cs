using System.Net;
using FaucetSharp.Shared.packets;

namespace FaucetSharp.Shared.events.client;

public delegate void HeartbeatEvent(HeartbeatEventArgs args);

public sealed class HeartbeatEventArgs : AbstractEventPacketArgs
{
    public HeartbeatEventArgs(IPEndPoint sender, IHeartbeatPacket packet) : base(sender, packet)
    {
    }
}