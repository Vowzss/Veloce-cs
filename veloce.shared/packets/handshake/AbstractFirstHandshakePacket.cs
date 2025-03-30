using ProtoBuf;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractFirstHandshakePacket : AbstractPacket, IFirstHandshakePacket
{
    [ProtoMember(3)]
    public required byte[] PublicKey { get; init; } = null!;

    // Protobuf serialization
    protected AbstractFirstHandshakePacket() { }

    protected AbstractFirstHandshakePacket(byte[] key)
    {
        PublicKey = key;
    }
}