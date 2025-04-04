using FaucetSharp.Shared.attributes;
using ProtoBuf;

namespace FaucetSharp.Shared.packets;

[ProtoContract]
[PacketIdentifier("veloce.pkt.heartbeat")]
public sealed class VeloceHeartbeatPacket : AbstractHeartbeatPacket
{
    public VeloceHeartbeatPacket(string playerId) : base(playerId)
    {
    }
}