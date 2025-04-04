using FaucetSharp.Shared.attributes;

namespace FaucetSharp.Shared.packets;

[PacketIdentifier("veloce.pkt.connect")]
public sealed class VeloceConnectPacket : AbstractConnectPacket
{
    public VeloceConnectPacket(string playerId) : base(playerId)
    {
    }
}