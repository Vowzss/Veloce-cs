using veloce.shared.events;
using veloce.shared.events.client;
using veloce.shared.handlers;
using veloce.shared.models;
using veloce.shared.packets;

namespace veloce.shared.interceptors.client;

public abstract class AbstractClientPacketInterceptor : IClientPacketInterceptor
{
    public IPacketDeserializer Deserializer { get; }

    public event HandshakeEvent OnHandshake;

    public event HeartbeatEvent OnHeartbeat;
    
    protected AbstractClientPacketInterceptor(ref IPacketDeserializer deserializer)
    {
        Deserializer = deserializer;
    }

    public void Accept(DataReceiveArgs args, IEncryptionContext encryption)
    {
        // Deserialize packet
        var packet = Deserializer.Read(args.Data, encryption);
        
        // Match against default packets
        switch (packet)
        {
            case IHandshakePacket p:
                OnHandshake.Invoke(new HandshakeEventArgs(args.Sender, p));
                break;
            
            case IHeartbeatPacket p:
                OnHeartbeat.Invoke(new HeartbeatEventArgs(args.Sender, p));
                break;

            default:
                Handle(new EventPacketArgs(args.Sender, packet));
                break;
        }
    }

    public virtual void Handle(IEventPacketArgs args)
    {
        return;
    }
}