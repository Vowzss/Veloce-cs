using System.Net;
using FaucetSharp.Gameplay.handlers;
using FaucetSharp.Gameplay.interceptors;
using FaucetSharp.Shared.channels.server;
using FaucetSharp.Shared.enums;
using FaucetSharp.Shared.exceptions;
using FaucetSharp.Shared.models;
using FaucetSharp.Shared.packets;

namespace FaucetSharp.Gameplay.channels;

public sealed class FaucetServerChannel : AbstractServerChannel
{
    public FaucetServerChannel(IPEndPoint endPoint, IServerConfig config) : base(endPoint, config)
    {
        Serializer = new FaucetPacketSerializer();
        PacketInterceptor = new FaucetServerPacketInterceptor(new FaucetPacketDeserializer());
        SessionHandler = new FaucetServerSessionHandler();
        
        // Bind default handshake callback
        PacketInterceptor.OnHandshake += async args =>
        {
            var packet = args.Packet as IHandshakePacket;
            
            var session = SessionHandler.FindByEndpoint(args.Sender);
            if (session == null) throw new SessionNotFoundException();

            switch (packet!.Step)
            {
                case HandshakeStep.PublicKey:
                    await Send(session, new FaucetHandshakePacket
                    {
                        Key = Config.Rsa.ExportRSAPublicKey(),
                        Step = HandshakeStep.Establishing
                    });
                    break;
                case HandshakeStep.AesKey:
                    session.Encryption.LoadAesKey(Config.Rsa, packet.Key!);
                    await Send(session, new FaucetHandshakePacket
                    {
                        Step = HandshakeStep.Established
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        };

        PacketInterceptor.OnConnect += args =>
        {
            var session = SessionHandler.FindByEndpoint(args.Sender) as AbstractServerSession;
            if (session == null) throw new SessionNotFoundException();

            session.Status = ClientStatus.Connected;
        };

        PacketInterceptor.OnDisconnect += args =>
        {
            var session = SessionHandler.FindByEndpoint(args.Sender) as AbstractServerSession;
            if (session == null) throw new SessionNotFoundException();

            session.Status = ClientStatus.Disconnected;
        };
    }
}