using FaucetSharp.Models.Attributes;
using FaucetSharp.Models.Packets.Reconnect;

namespace FaucetSharp.Core.Packets;

[PacketIdentifier("veloce.pkt.reconnect")]
public sealed class FaucetReconnectPacket : AbstractReconnectPacket
{
    public FaucetReconnectPacket(string playerId) : base(playerId)
    {
    }
}