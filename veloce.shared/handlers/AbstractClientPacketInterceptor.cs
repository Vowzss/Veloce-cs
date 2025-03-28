using veloce.shared.events;
using veloce.shared.packets;
using veloce.shared.utils;

namespace veloce.shared.handlers;

public abstract class AbstractClientPacketInterceptor : IClientPacketInterceptor
{
    public PingEvent? OnPing { get; set; }
    
    public void Accept(byte[] data)
    {
        // Deserialize packet
        var packet = PacketHandler.Read(data);
        
        // Match against default packets
        switch (packet)
        {
            case IPingPacket p:
                OnPing?.Invoke(p);
                return;
            
            default:
                Handle(packet);
                return;
        }
    }

    public abstract void Handle(IPacket packet);
}