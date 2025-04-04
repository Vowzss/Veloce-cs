using System.Net;
using veloce.shared.packets;

namespace veloce.shared.events;

public delegate void ConnectEvent(ConnectEventArgs args);

public sealed class ConnectEventArgs : AbstractEventPacketArgs
{
    public ConnectEventArgs(IPEndPoint sender, IConnectPacket packet) : base(sender, packet)
    {
    }
}