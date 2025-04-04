using ProtoBuf;

namespace FaucetSharp.Shared.packets;

[ProtoContract]
public interface IReconnectPacket : IGamePacket
{
}