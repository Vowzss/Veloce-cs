using ProtoBuf;
using veloce.shared.attributes;
using veloce.shared.enums;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractHeartbeatPacket : AbstractGamePacket, IHeartbeatPacket
{
    public required HeartbeatStep Step { get; init; }
}

[ProtoContract]
[PacketIdentifier("veloce.pkt.heartbeat")]
public sealed class VeloceHeartbeatPacket : AbstractHeartbeatPacket
{
    
}