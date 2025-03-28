using ProtoBuf;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractSecondHandshakePacket : AbstractPacket, ISecondHandshakePacket
{
    [ProtoMember(3)]
    public string AesKey { get; init; } = null!;

    // Protobuf serialization
    protected AbstractSecondHandshakePacket() { }

    protected AbstractSecondHandshakePacket(string key)
    {
        AesKey = key;
    }
}