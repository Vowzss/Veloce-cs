using ProtoBuf;
using veloce.shared.attributes;
using veloce.shared.enums;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractHandshakePacket : AbstractPacket, IHandshakePacket
{
    [ProtoMember(3)]
    public byte[]? Key { get; init; }
    
    [ProtoMember(4)]
    public required HandshakeStep Step { get; init; }
    
    // Protobuf serialization
    protected AbstractHandshakePacket() { }

    protected AbstractHandshakePacket(HandshakeStep step)
    {
        Step = step;
    }
}

[ProtoContract]
[PacketIdentifier("veloce.pkt.handshake")]
public sealed class VeloceHandshakePacket : AbstractHandshakePacket
{
    
}