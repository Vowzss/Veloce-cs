using FaucetSharp.Models.Attributes;
using FaucetSharp.Models.Packets.Connect;

namespace FaucetSharp.Core.Packets;

[PacketIdentifier("faucet.pkt.connect")]
public sealed class FaucetConnectPacket(string playerId) : AbstractConnectPacket(playerId)
{
    // Protobuf
    private FaucetConnectPacket() : this(string.Empty)
    {
    }
}