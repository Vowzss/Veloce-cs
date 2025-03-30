using ProtoBuf;

namespace veloce.shared.packets;

[ProtoContract]
public interface IFirstHandshakePacket : IPacket
{
    public byte[] PublicKey { get; }
}