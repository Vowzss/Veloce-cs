using FaucetSharp.Shared.attributes;
using ProtoBuf;

namespace FaucetSharp.Shared.packets;

[ProtoContract]
[PacketIdentifier("veloce.pkt.handshake")]
public sealed class VeloceHandshakePacket : AbstractHandshakePacket
{
}