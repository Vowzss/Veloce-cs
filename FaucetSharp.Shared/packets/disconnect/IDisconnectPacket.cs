using ProtoBuf;

namespace FaucetSharp.Shared.packets;

[ProtoContract]
public interface IDisconnectPacket : IGamePacket
{
}