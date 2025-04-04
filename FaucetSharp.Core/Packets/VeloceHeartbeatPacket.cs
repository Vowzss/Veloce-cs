using FaucetSharp.Models.Attributes;
using FaucetSharp.Models.Packets.Heartbeat;
using ProtoBuf;

namespace FaucetSharp.Core.Packets;

[ProtoContract]
[PacketIdentifier("veloce.pkt.heartbeat")]
public sealed class FaucetHeartbeatPacket : AbstractHeartbeatPacket
{
    public FaucetHeartbeatPacket(string playerId) : base(playerId)
    {
    }
}