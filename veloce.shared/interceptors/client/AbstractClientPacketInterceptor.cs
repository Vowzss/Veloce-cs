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

    public void Accept(DataReceiveArgs args, EncryptionContext? encryption)
    {
        // Deserialize packet
        var packet = Deserializer.Read(args.Data, encryption);

        // Match against default packets
        switch (packet)
        {
            case IHandshakePacket p:
                OnHandshake.Invoke(new HandshakeEventArgs(args.Sender, p));
                return;
            
            case IHeartbeatPacket p:
                OnHeartbeat.Invoke(new HeartbeatEventArgs(args.Sender, p));
                return;
            
            default:
                Handle(new EventPacketArgs(args.Sender, packet));
                return;
        }
    }

    public virtual void Handle(IEventPacketArgs args) { }
}

public sealed class DefaultClientPacketInterceptor : AbstractClientPacketInterceptor
{
    public DefaultClientPacketInterceptor(IPacketDeserializer deserializer) : base(ref deserializer)
    {
    }
}