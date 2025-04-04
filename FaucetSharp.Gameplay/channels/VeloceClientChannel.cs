using System.Net;
using FaucetSharp.Gameplay.encryption;
using FaucetSharp.Gameplay.handlers;
using FaucetSharp.Gameplay.interceptors;
using FaucetSharp.Shared.channels.client;
using FaucetSharp.Shared.enums;
using FaucetSharp.Shared.models;
using FaucetSharp.Shared.packets;

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