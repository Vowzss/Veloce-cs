using veloce.shared.events;
using veloce.shared.events.client;
using veloce.shared.handlers;
using veloce.shared.models;
using veloce.shared.packets;

namespace veloce.shared.interceptors.server;

public abstract class AbstractServerPacketInterceptor : IServerPacketInterceptor
{
    public IPacketDeserializer Deserializer { get; }

    public event FirstHandshakeEvent OnFirstHandshake;
    public event SecondHandshakeEvent OnSecondHandshake;

    public event ConnectEvent OnConnect;
    public event DisconnectEvent OnDisconnect;
    public event ReconnectEvent OnReconnect;

    public event PongEvent OnPong;
    
    protected AbstractServerPacketInterceptor(ref IPacketDeserializer deserializer)
    {
        Deserializer = deserializer;
    }
    
    // TODO: handle when encryption context not set yet when no handshake done first
    public void Accept(DataReceiveArgs args, EncryptionContext? encryption)
    {
        // Deserialize packet
        var packet = Deserializer.Read(args.Data, encryption);

        // Match against default packets
        switch (packet)
        {
            case IFirstHandshakePacket p:
                OnFirstHandshake.Invoke(new FirstHandshakeEventArgs(args.Sender, p));
                return;
            
            case ISecondHandshakePacket p:
                OnSecondHandshake.Invoke(new SecondHandshakeEventArgs(args.Sender, p));
                return;
            
            case IConnectPacket p:
                OnConnect.Invoke(new ConnectEventArgs(args.Sender, p));
                return;
           
            case IDisconnectPacket p:
                OnDisconnect.Invoke(new DisconnectEventArgs(args.Sender, p));
                return;
           
            case IReconnectPacket p:
                OnReconnect.Invoke(new ReconnectEventArgs(args.Sender, p));
                return;
            
            case IPongPacket p:
                OnPong.Invoke(new PongEventArgs(args.Sender, p));
                return;
            
            default:
                Handle(new EventPacketArgs(args.Sender, packet));
                return;
        }
    }

    public virtual void Handle(IEventPacketArgs args) { }
}

public sealed class DefaultServerPacketInterceptor : AbstractServerPacketInterceptor
{
    public DefaultServerPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
        
    }
}