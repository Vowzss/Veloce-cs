using Serilog;
using veloce.shared.interceptors.server;
using veloce.shared.packets;
using veloce.shared.utils;
using veloce.tests.packets;

namespace veloce.tests;

public partial class ServerPacketInterceptor : AbstractServerPacketInterceptor
{
    public ServerPacketInterceptor()
    {
        PacketHandler.RegisterPacketType<AbstractPacket, ConnectPacket>();
        PacketHandler.RegisterPacketType<AbstractPacket, PositionPacket>();
    }
    
    public override void Accept(IPacket packet)
    {
        switch (packet)
        {
            case IPositionPacket p:
                HandlePositionPacket(p);
                return;
        }
    }
    
    private void HandlePositionPacket(IPositionPacket packet)
    {
        Log.Information("IPositionPacket");
    }
}