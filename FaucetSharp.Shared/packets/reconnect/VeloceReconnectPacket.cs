using FaucetSharp.Shared.attributes;

namespace FaucetSharp.Shared.packets;

[PacketIdentifier("veloce.pkt.reconnect")]
public sealed class FaucetReconnectPacket : AbstractReconnectPacket
{
    public FaucetReconnectPacket(string playerId) : base(playerId)
    {
    }
}