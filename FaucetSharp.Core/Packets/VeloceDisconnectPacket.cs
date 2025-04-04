using FaucetSharp.Models.Attributes;
using FaucetSharp.Models.Packets.Disconnect;

namespace FaucetSharp.Core.Packets;

[PacketIdentifier("veloce.pkt.disconnect")]
public sealed class FaucetDisconnectPacket : AbstractDisconnectPacket
{
    public FaucetDisconnectPacket(string playerId) : base(playerId)
    {
    }
}