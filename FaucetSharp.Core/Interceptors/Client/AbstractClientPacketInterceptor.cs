using FaucetSharp.Core.Handlers;

using FaucetSharp.Models.Events;
using FaucetSharp.Models.Events.Args;
using FaucetSharp.Models.Objects.Encryption;
using FaucetSharp.Models.Packets.Handshake;
using FaucetSharp.Models.Packets.Heartbeat;

namespace FaucetSharp.Core.interceptors.client;

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
        Console.WriteLine($"Client accepted packet:[[{packet.Identifier}]]");
        
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