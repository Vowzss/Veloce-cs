using veloce.shared.attributes;

namespace veloce.shared.packets;

[PacketIdentifier("veloce.pkt.disconnect")]
public sealed class VeloceDisconnectPacket : AbstractDisconnectPacket
{
    public VeloceDisconnectPacket(string playerId) : base(playerId)
    {
    }
}