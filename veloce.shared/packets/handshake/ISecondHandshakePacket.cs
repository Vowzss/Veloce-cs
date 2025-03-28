using ProtoBuf;

namespace veloce.shared.packets;

[ProtoContract]
public interface ISecondHandshakePacket : IPacket
{
    public string AesKey { get; }
}