using FaucetSharp.Shared.attributes;

namespace FaucetSharp.Shared.packets;

[PacketIdentifier("veloce.pkt.connect")]
public sealed class FaucetConnectPacket : AbstractConnectPacket
{
    public FaucetConnectPacket(string playerId) : base(playerId)
    {
    }
}