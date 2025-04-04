using veloce.shared.attributes;

namespace veloce.shared.packets;

[PacketIdentifier("veloce.pkt.reconnect")]
public sealed class VeloceReconnectPacket : AbstractReconnectPacket
{
    public VeloceReconnectPacket(string playerId) : base(playerId)
    {
    }
}