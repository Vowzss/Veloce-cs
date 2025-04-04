using ProtoBuf;

namespace FaucetSharp.Models.Packets.Disconnect;

[ProtoContract]
public interface IDisconnectPacket : IGamePacket
{
}