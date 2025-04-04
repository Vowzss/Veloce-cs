using System.Net;

using FaucetSharp.Models.Events.Args;
using FaucetSharp.Models.Packets.Connect;

namespace FaucetSharp.Models.Events;

public delegate void ConnectEvent(ConnectEventArgs args);

public sealed class ConnectEventArgs : AbstractEventPacketArgs
{
    public ConnectEventArgs(IPEndPoint sender, IConnectPacket packet) : base(sender, packet)
    {
    }
}