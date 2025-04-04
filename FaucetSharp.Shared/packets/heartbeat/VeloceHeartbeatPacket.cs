using FaucetSharp.Shared.attributes;
using ProtoBuf;

namespace FaucetSharp.Shared.packets;

[ProtoContract]
[PacketIdentifier("veloce.pkt.heartbeat")]
public sealed class FaucetHeartbeatPacket : AbstractHeartbeatPacket
{
    public FaucetHeartbeatPacket(string playerId) : base(playerId)
    {
    }
}