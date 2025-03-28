using veloce.shared.events;
using veloce.shared.events.server;
using veloce.shared.handlers;
using veloce.shared.models;
using veloce.shared.packets;
using veloce.shared.utils;

namespace veloce.shared.interceptors.server;

public abstract class AbstractServerPacketInterceptor: IServerPacketInterceptor
{
    public IPacketDeserializer Deserializer { get; }
    
    public FirstHandshakeEvent? OnFirstHandshake { get; init; }
    public SecondHandshakeEvent? OnSecondHandshake { get; init; }

    public ConnectEvent? OnConnect { get; init; }
    public DisconnectEvent? OnDisconnect { get; init; }
    public ReconnectEvent? OnReconnect { get; init; }
    
    public PongEvent? OnPong { get; init; }
    
    protected AbstractServerPacketInterceptor(ref IPacketDeserializer deserializer)
    {
        Deserializer = deserializer;
    }
    
    // TODO: handle when encryption context not set yet when no handshake done first
    public void Accept(byte[] data, EncryptionContext? encryption)
    {
        // Deserialize packet
        var packet = Deserializer.Read(data, encryption);

        // Match against default packets
        switch (packet)
        {
            case IFirstHandshakePacket p:
                OnFirstHandshake?.Invoke(p);
                return;
            
            case ISecondHandshakePacket p:
                OnSecondHandshake?.Invoke(p);
                return;
            
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

    public virtual void Handle(IPacket packet) { }
}

public sealed class DefaultServerPacketInterceptor : AbstractServerPacketInterceptor
{
    public DefaultServerPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
    }
}