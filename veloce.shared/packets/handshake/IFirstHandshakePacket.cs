using ProtoBuf;

namespace veloce.shared.packets;

[ProtoContract]
public interface IFirstHandshakePacket : IPacket
{
    public string PublicKey { get; }
}