using FaucetSharp.Models.Attributes;
using FaucetSharp.Models.Enums;
using FaucetSharp.Models.Packets.Handshake;
using ProtoBuf;

namespace FaucetSharp.Core.Packets;

[ProtoContract]
[PacketIdentifier("faucet.pkt.handshake")]
public sealed class FaucetHandshakePacket(HandshakeStep step) : AbstractHandshakePacket(step)
{
    // Protobuf
    private FaucetHandshakePacket() : this(HandshakeStep.Unknown)
    {
    }
}