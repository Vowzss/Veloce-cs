using Serilog.Core;
using veloce.shared.events;
using veloce.shared.events.client;
using veloce.shared.handlers;
using veloce.shared.models;
using veloce.shared.packets;
using veloce.shared.utils;

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
            
            default:
                return Handle(new EventPacketArgs(args.Sender, packet));
        }
    }

    public virtual bool Handle(IEventPacketArgs args)
    {
        return false;
    }
}

public sealed class DefaultClientPacketInterceptor : AbstractClientPacketInterceptor
{
    public DefaultClientPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
    }
}