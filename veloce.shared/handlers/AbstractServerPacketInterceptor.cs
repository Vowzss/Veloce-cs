using ProtoBuf.Meta;
using Serilog;
using veloce.shared.events;
using veloce.shared.packets;
using veloce.shared.utils;

namespace veloce.shared.handlers;

public abstract class AbstractServerPacketInterceptor: IServerPacketInterceptor
{
    public ConnectEvent? OnConnect { get; set; }
    public DisconnectEvent? OnDisconnect { get; set; }
    public ReconnectEvent? OnReconnect { get; set; }
    
    public PongEvent? OnPong { get; set; }

    public void Accept(byte[] data)
    {
        // Deserialize packet
        var packet = PacketHandler.Read(data);

        // Match against default packets
        switch (packet)
        {
            case IConnectPacket p:
                OnConnect?.Invoke(p);
                return;
           
            case IDisconnectPacket p:
                OnDisconnect?.Invoke(p);
                return;
           
            case IReconnectPacket p:
                OnReconnect?.Invoke(p);
                return;
            
            case IPongPacket p:
                OnPong?.Invoke(p);
                return;
            
            default:
                Handle(packet);
                return;
        }
    }
    
    public abstract void Handle(IPacket packet);
}