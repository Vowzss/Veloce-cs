using FaucetSharp.Models.Enums;
using ProtoBuf;

namespace FaucetSharp.Models.Packets.Heartbeat;

[ProtoContract]
public interface IHeartbeatPacket : IGamePacket
{
    protected HeartbeatStep Step { get; }
}