using veloce.shared.events.client;
using veloce.shared.packets;
using veloce.shared.utils;

namespace veloce.shared.interceptors.client;

public abstract class AbstractClientPacketInterceptor : IClientPacketInterceptor
{
    public IPacketDeserializer Deserilizer { get; }
    
    public PingEvent? OnPing { get; set; }
    
    protected AbstractClientPacketInterceptor(IPacketDeserializer deserilizer)
    {
        Deserilizer = deserilizer;
    }

    public void Accept(byte[] data)
    {
        // Deserialize packet
        var packet = Deserilizer.Read(data);
        
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