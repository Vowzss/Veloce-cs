using FaucetSharp.Models.Attributes;
using FaucetSharp.Models.Packets.Reconnect;

namespace FaucetSharp.Core.Packets;

[PacketIdentifier("faucet.pkt.reconnect")]
public sealed class FaucetReconnectPacket(string playerId) : AbstractReconnectPacket(playerId)
{
    // Protobuf
    private FaucetReconnectPacket() : this(string.Empty)
    {
    }
}