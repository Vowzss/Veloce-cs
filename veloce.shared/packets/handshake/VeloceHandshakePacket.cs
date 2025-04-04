using ProtoBuf;
using veloce.shared.attributes;

namespace veloce.shared.packets;

[ProtoContract]
[PacketIdentifier("veloce.pkt.handshake")]
public sealed class VeloceHandshakePacket : AbstractHandshakePacket
{
}