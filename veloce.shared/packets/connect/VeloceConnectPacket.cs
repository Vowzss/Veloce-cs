using veloce.shared.attributes;

namespace veloce.shared.packets;

[PacketIdentifier("veloce.pkt.connect")]
public sealed class VeloceConnectPacket : AbstractConnectPacket
{
    public VeloceConnectPacket(string playerId) : base(playerId)
    {
    }
}