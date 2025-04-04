using System.Net;
using FaucetSharp.Shared.packets;

namespace FaucetSharp.Shared.events;

public delegate void DisconnectEvent(DisconnectEventArgs args);

public sealed class DisconnectEventArgs : AbstractEventPacketArgs
{
    public DisconnectEventArgs(IPEndPoint sender, IDisconnectPacket packet) : base(sender, packet)
    {
    }
}