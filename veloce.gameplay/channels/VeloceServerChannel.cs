using System.Net;
using veloce.gameplay.handlers;
using veloce.gameplay.interceptors;
using veloce.shared.channels.server;
using veloce.shared.enums;
using veloce.shared.exceptions;
using veloce.shared.models;
using veloce.shared.packets;

namespace veloce.gameplay.channels;

public sealed class VeloceServerChannel : AbstractServerChannel
{
    public VeloceServerChannel(IPEndPoint endPoint, IServerConfig config) : base(endPoint, config)
    {
        Serializer = new VelocePacketSerializer();
        PacketInterceptor = new VeloceServerPacketInterceptor(new VelocePacketDeserializer());
        SessionHandler = new VeloceServerSessionHandler();
        
        // Bind default handshake callback
        PacketInterceptor.OnHandshake += async args =>
        {
            var packet = args.Packet as IHandshakePacket;
            
            var session = SessionHandler.FindByEndpoint(args.Sender);
            if (session == null) throw new SessionNotFoundException();

            switch (packet!.Step)
            {
                case HandshakeStep.PublicKey:
                    await Send(session, new VeloceHandshakePacket
                    {
                        Key = Config.Rsa.ExportRSAPublicKey(),
                        Step = HandshakeStep.Establishing
                    });
                    break;
                case HandshakeStep.AesKey:
                    session.Encryption.LoadAesKey(Config.Rsa, packet.Key!);
                    await Send(session, new VeloceHandshakePacket
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