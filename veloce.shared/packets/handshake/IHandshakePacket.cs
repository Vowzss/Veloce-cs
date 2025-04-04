using ProtoBuf;
using veloce.shared.enums;

namespace veloce.shared.packets;

[ProtoContract]
public interface IHandshakePacket : IPacket
{
    public byte[]? Key { get; }

    public HandshakeStep Step { get; }
}