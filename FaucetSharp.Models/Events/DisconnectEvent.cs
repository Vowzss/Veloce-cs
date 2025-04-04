using System.Net;

using FaucetSharp.Models.Events.Args;
using FaucetSharp.Models.Packets.Disconnect;

namespace FaucetSharp.Models.Events;

public delegate void DisconnectEvent(DisconnectEventArgs args);

public sealed class DisconnectEventArgs : AbstractEventPacketArgs
{
    public DisconnectEventArgs(IPEndPoint sender, IDisconnectPacket packet) : base(sender, packet)
    {
    }
}