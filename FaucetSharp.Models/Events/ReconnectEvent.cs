using System.Net;
using FaucetSharp.Models.Events.Args;
using FaucetSharp.Models.Packets.Reconnect;

namespace FaucetSharp.Models.Events;

public delegate void ReconnectEvent(ReconnectEventArgs args);

public sealed class ReconnectEventArgs : AbstractEventPacketArgs
{
    public ReconnectEventArgs(IPEndPoint sender, IReconnectPacket packet) : base(sender, packet)
    {
    }
}