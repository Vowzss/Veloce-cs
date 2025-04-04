using System.Net;
using FaucetSharp.Core.Channels.Server;
using FaucetSharp.Core.Packets;
using FaucetSharp.Gameplay.Handlers;
using FaucetSharp.Gameplay.Interceptors;
using FaucetSharp.Models.Enums;
using FaucetSharp.Models.Objects.Config.Server;
using FaucetSharp.Models.Objects.Session.Server;
using FaucetSharp.Models.Packets.Handshake;
using FaucetSharp.Shared.Exceptions;

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
                    await Send(session, new FaucetHandshakePacket(HandshakeStep.Establishing) {
                        Key = Config.Rsa.ExportRSAPublicKey(),
                    });
                    break;
                case HandshakeStep.AesKey:
                    session.Encryption.LoadAesKey(Config.Rsa, packet.Key!);
                    await Send(session, new FaucetHandshakePacket(HandshakeStep.Established));
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