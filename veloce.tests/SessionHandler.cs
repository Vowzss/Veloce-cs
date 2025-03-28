using System.Net.Sockets;
using veloce.shared.handlers;

namespace veloce.tests;

public class SessionHandler : AbstractSessionHandler<DemoSession>
{
    public override DemoSession CreateSession(UdpReceiveResult rs)
    {
        return new DemoSession(rs.RemoteEndPoint);
    }
}