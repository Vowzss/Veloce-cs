using FaucetSharp.Core.Handlers;

using FaucetSharp.Models.Events;
using FaucetSharp.Models.Events.Args;
using FaucetSharp.Models.Objects.Encryption;
using FaucetSharp.Models.Packets.Connect;
using FaucetSharp.Models.Packets.Disconnect;
using FaucetSharp.Models.Packets.Handshake;
using FaucetSharp.Models.Packets.Heartbeat;
using FaucetSharp.Models.Packets.Reconnect;

namespace FaucetSharp.Core.interceptors.client;

public abstract class AbstractClientPacketInterceptor : IClientPacketInterceptor
{
    public IPacketDeserializer Deserializer { get; }

    public event HandshakeEvent OnHandshake;
    public event HeartbeatEvent OnHeartbeat;
    
    /// <summary>
    ///     Event fired whenever the server acknowledged the connection.
    /// </summary>
    public event ConnectEvent OnConnect;
    
    /// <summary>
    ///     Event fired whenever the server acknowledged the disconnection.
    /// </summary>
    public event DisconnectEvent OnDisconnect;
    
    /// <summary>
    ///     Event fired whenever the server acknowledged the reconnection.
    /// </summary>
    public event ReconnectEvent OnReconnect;
    
    protected AbstractClientPacketInterceptor(ref IPacketDeserializer deserializer)
    {
        Deserializer = deserializer;
    }

    public void Accept(DataReceiveArgs args, IEncryptionContext encryption)
    {
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

            case IConnectPacket p:
                OnConnect.Invoke(new ConnectEventArgs(args.Sender, p));
                break;

            case IDisconnectPacket p:
                OnDisconnect.Invoke(new DisconnectEventArgs(args.Sender, p));
                break;

            case IReconnectPacket p:
                OnReconnect.Invoke(new ReconnectEventArgs(args.Sender, p));
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