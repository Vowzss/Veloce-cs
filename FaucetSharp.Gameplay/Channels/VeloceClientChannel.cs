using System.Net;
using FaucetSharp.Core.Channels.Client;
using FaucetSharp.Core.Packets;
using FaucetSharp.Gameplay.Encryption;
using FaucetSharp.Gameplay.Handlers;
using FaucetSharp.Gameplay.Interceptors;
using FaucetSharp.Models.Enums;
using FaucetSharp.Models.Objects.Config.Client;
using FaucetSharp.Models.Packets.Handshake;

namespace FaucetSharp.Gameplay.channels;

public sealed class FaucetClientChannel : AbstractClientChannel
{
    public FaucetClientChannel(IPEndPoint endPoint, IClientConfig config) : base(endPoint, config, new FaucetClientEncryption())
    {
        Serializer = new FaucetPacketSerializer();
        PacketInterceptor = new FaucetClientPacketInterceptor(new FaucetPacketDeserializer());
            
        // Bind default handshake callback
        PacketInterceptor.OnHandshake += async args =>
        {
            var packet = args.Packet as IHandshakePacket;
            
            switch (packet!.Step)
            {
                case HandshakeStep.Establishing:
                    await Send(new FaucetHandshakePacket
                    {
                        Key = Encryption.GenerateAesKey(packet.Key!),
                        Step = HandshakeStep.AesKey
                    });
                    break;
                case HandshakeStep.Established:
                    Logger.Information("Handshake established with server.");
                    await Send(new FaucetConnectPacket(config.PlayerId.ToString()));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        };
    }
}