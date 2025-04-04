using FaucetSharp.Models.Enums;
using ProtoBuf;

namespace FaucetSharp.Models.Packets.Heartbeat;

[ProtoContract]
public abstract class AbstractHeartbeatPacket : AbstractGamePacket, IHeartbeatPacket
{
    public required HeartbeatStep Step { get; init; }
    
    protected AbstractHeartbeatPacket(string playerId) : base(playerId)
    {
    }
}