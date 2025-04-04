using ProtoBuf;

namespace FaucetSharp.Models.Packets.Reconnect;

[ProtoContract]
public interface IReconnectPacket : IGamePacket
{
}