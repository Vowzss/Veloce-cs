using ProtoBuf;
using veloce.shared.enums;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractHeartbeatPacket : AbstractGamePacket, IHeartbeatPacket
{
    public required HeartbeatStep Step { get; init; }
    
    protected AbstractHeartbeatPacket(string playerId) : base(playerId)
    {
    }
}