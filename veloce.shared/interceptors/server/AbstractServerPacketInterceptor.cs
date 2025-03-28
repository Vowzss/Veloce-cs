using veloce.shared.events.server;
using veloce.shared.packets;
using veloce.shared.utils;

namespace veloce.shared.interceptors.server;

public abstract class AbstractServerPacketInterceptor: IServerPacketInterceptor
{
    public IPacketDeserializer Deserilizer { get; }

    public ConnectEvent? OnConnect { get; set; }
    public DisconnectEvent? OnDisconnect { get; set; }
    public ReconnectEvent? OnReconnect { get; set; }
    
    public PongEvent? OnPong { get; set; }
    
    protected AbstractServerPacketInterceptor(ref IPacketDeserializer deserilizer)
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