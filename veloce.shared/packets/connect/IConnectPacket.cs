using ProtoBuf;
using veloce.shared.attributes;

namespace veloce.shared.packets;

[ProtoContract]
public interface IConnectPacket : IGamePacket
{
}