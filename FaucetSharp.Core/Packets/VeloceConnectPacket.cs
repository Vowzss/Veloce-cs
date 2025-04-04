using FaucetSharp.Models.Attributes;
using FaucetSharp.Models.Packets.Connect;

namespace FaucetSharp.Core.Packets;

[PacketIdentifier("veloce.pkt.connect")]
public sealed class FaucetConnectPacket : AbstractConnectPacket
{
    public FaucetConnectPacket(string playerId) : base(playerId)
    {
    }
}