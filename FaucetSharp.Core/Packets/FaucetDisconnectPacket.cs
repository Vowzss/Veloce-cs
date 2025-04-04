using FaucetSharp.Models.Attributes;
using FaucetSharp.Models.Packets.Disconnect;

namespace FaucetSharp.Core.Packets;

[PacketIdentifier("faucet.pkt.disconnect")]
public sealed class FaucetDisconnectPacket(string playerId) : AbstractDisconnectPacket(playerId)
{
    // Protobuf
    private FaucetDisconnectPacket() : this(string.Empty)
    {
    }
}