using FaucetSharp.Shared.enums;
using ProtoBuf;

namespace FaucetSharp.Shared.packets;

[ProtoContract]
public interface IHeartbeatPacket : IGamePacket
{
    protected HeartbeatStep Step { get; }
}