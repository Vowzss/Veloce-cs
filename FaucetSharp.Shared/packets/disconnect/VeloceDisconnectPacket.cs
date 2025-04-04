using FaucetSharp.Shared.attributes;

namespace FaucetSharp.Shared.packets;

[PacketIdentifier("veloce.pkt.disconnect")]
public sealed class VeloceDisconnectPacket : AbstractDisconnectPacket
{
    public VeloceDisconnectPacket(string playerId) : base(playerId)
    {
    }
}