using FaucetSharp.Shared.enums;
using ProtoBuf;

namespace FaucetSharp.Shared.packets;

[ProtoContract]
public abstract class AbstractHeartbeatPacket : AbstractGamePacket, IHeartbeatPacket
{
    public required HeartbeatStep Step { get; init; }
    
    protected AbstractHeartbeatPacket(string playerId) : base(playerId)
    {
    }
}