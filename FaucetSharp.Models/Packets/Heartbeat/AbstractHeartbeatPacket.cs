using FaucetSharp.Models.Enums;
using ProtoBuf;

namespace FaucetSharp.Models.Packets.Heartbeat;

[ProtoContract]
public abstract class AbstractHeartbeatPacket : AbstractGamePacket, IHeartbeatPacket
{
    public HeartbeatStep Step { get; init; }
    
    protected AbstractHeartbeatPacket(string playerId, HeartbeatStep step) : base(playerId)
    {
        Step = step;
    }
}