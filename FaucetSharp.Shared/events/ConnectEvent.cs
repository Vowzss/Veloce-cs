using System.Net;
using FaucetSharp.Shared.packets;

namespace FaucetSharp.Shared.events;

public delegate void ConnectEvent(ConnectEventArgs args);

public sealed class ConnectEventArgs : AbstractEventPacketArgs
{
    public ConnectEventArgs(IPEndPoint sender, IConnectPacket packet) : base(sender, packet)
    {
    }
}