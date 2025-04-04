using FaucetSharp.Models.Attributes;
using FaucetSharp.Models.Packets;
using ProtoBuf;

namespace FaucetSharp.Core.Packets;

[ProtoContract]
[PacketIdentifier("faucet.pkt.ack")]
public sealed class FaucetAckPacket : AbstractAckPacket
{
    private FaucetAckPacket() : base(false)
    {
    }
    
    public FaucetAckPacket(bool isSuccessful) : base(isSuccessful)
    {
    }
}