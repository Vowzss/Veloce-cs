using veloce.shared.events;
using veloce.shared.events.client;
using veloce.shared.handlers;
using veloce.shared.models;
using veloce.shared.packets;

namespace veloce.shared.interceptors.server;

public abstract class AbstractServerPacketInterceptor : IServerPacketInterceptor
{
    public IPacketDeserializer Deserializer { get; }

    public event HandshakeEvent OnHandshake;

    public event ConnectEvent OnConnect;
    public event DisconnectEvent OnDisconnect;
    public event ReconnectEvent OnReconnect;

    public event HeartbeatEvent OnHeartbeat;
    
    protected AbstractServerPacketInterceptor(ref IPacketDeserializer deserializer)
    {
        Deserializer = deserializer;
    }
    
    public bool Accept(DataReceiveArgs args, EncryptionContext? encryption)
    {
        // Deserialize packet
        var packet = Deserializer.Read(args.Data, encryption);
        
        // Edge case: no encryption means the packet must be a handshake
        if (encryption == null || !encryption.IsValid())
        {
            if (packet is not IHandshakePacket) return false;
            OnHandshake.Invoke(new HandshakeEventArgs(args.Sender, (IHandshakePacket)packet));
            return true;
        }
        
        // Match against default packets
        switch (packet)
        {
            case IHeartbeatPacket p:
                OnHeartbeat.Invoke(new HeartbeatEventArgs(args.Sender, p));
                return true;
            
            case IConnectPacket p:
                OnConnect.Invoke(new ConnectEventArgs(args.Sender, p));
                return true;
           
            case IDisconnectPacket p:
                OnDisconnect.Invoke(new DisconnectEventArgs(args.Sender, p));
                return true;
           
            case IReconnectPacket p:
                OnReconnect.Invoke(new ReconnectEventArgs(args.Sender, p));
                return true;
            
            default:
                return Handle(new EventPacketArgs(args.Sender, packet));
        }
    }

    public virtual bool Handle(IEventPacketArgs args)
    {
        return false;
    }
}

public sealed class DefaultServerPacketInterceptor : AbstractServerPacketInterceptor
{
    public DefaultServerPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
        
    }
}