using FaucetSharp.Models.Attributes;
using FaucetSharp.Models.Enums;
using FaucetSharp.Models.Packets.Heartbeat;
using ProtoBuf;

namespace FaucetSharp.Core.Packets;

[ProtoContract]
[PacketIdentifier("faucet.pkt.heartbeat")]
public sealed class FaucetHeartbeatPacket(string playerId, HeartbeatStep step) : AbstractHeartbeatPacket(playerId, step)
{
    // Protobuf
    private FaucetHeartbeatPacket() : this(string.Empty, HeartbeatStep.Unknown)
    {
    }
}