using ProtoBuf;
using veloce.shared.enums;

namespace veloce.shared.packets;

[ProtoContract]
public interface IHandshakePacket : IPacket
{
    protected internal byte[]? Key { get; }

    protected internal HandshakeStep Step { get; }
}