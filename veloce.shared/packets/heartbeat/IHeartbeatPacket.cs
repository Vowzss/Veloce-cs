using ProtoBuf;
using veloce.shared.enums;

namespace veloce.shared.packets;

[ProtoContract]
public interface IHeartbeatPacket : IGamePacket
{
    protected internal HeartbeatStep Step { get; }
}