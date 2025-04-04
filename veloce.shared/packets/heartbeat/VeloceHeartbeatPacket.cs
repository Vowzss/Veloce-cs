using ProtoBuf;
using veloce.shared.attributes;

namespace veloce.shared.packets;

[ProtoContract]
[PacketIdentifier("veloce.pkt.heartbeat")]
public sealed class VeloceHeartbeatPacket : AbstractHeartbeatPacket
{
    public VeloceHeartbeatPacket(string playerId) : base(playerId)
    {
    }
}