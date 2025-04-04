using FaucetSharp.Shared.attributes;

namespace FaucetSharp.Shared.packets;

[PacketIdentifier("veloce.pkt.reconnect")]
public sealed class VeloceReconnectPacket : AbstractReconnectPacket
{
    public VeloceReconnectPacket(string playerId) : base(playerId)
    {
    }
}