using System.Net;
using FaucetSharp.Shared.packets;

namespace FaucetSharp.Shared.events;

public delegate void ReconnectEvent(ReconnectEventArgs args);

public sealed class ReconnectEventArgs : AbstractEventPacketArgs
{
    public ReconnectEventArgs(IPEndPoint sender, IReconnectPacket packet) : base(sender, packet)
    {
    }
}