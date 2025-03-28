using ProtoBuf;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractFirstHandshakePacket : AbstractPacket, IFirstHandshakePacket
{
    [ProtoMember(3)]
    public string PublicKey { get; init; } = null!;

    // Protobuf serialization
    protected AbstractFirstHandshakePacket() { }

    protected AbstractFirstHandshakePacket(string key)
    {
        PublicKey = key;
    }
}