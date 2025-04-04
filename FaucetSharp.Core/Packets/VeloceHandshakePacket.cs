using FaucetSharp.Models.Attributes;
using FaucetSharp.Models.Packets.Handshake;
using ProtoBuf;

namespace FaucetSharp.Core.Packets;

[ProtoContract]
[PacketIdentifier("veloce.pkt.handshake")]
public sealed class FaucetHandshakePacket : AbstractHandshakePacket
{
}