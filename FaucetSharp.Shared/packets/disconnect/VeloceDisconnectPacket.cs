using FaucetSharp.Shared.attributes;

namespace FaucetSharp.Shared.packets;

[PacketIdentifier("veloce.pkt.disconnect")]
public sealed class FaucetDisconnectPacket : AbstractDisconnectPacket
{
    public FaucetDisconnectPacket(string playerId) : base(playerId)
    {
    }
}