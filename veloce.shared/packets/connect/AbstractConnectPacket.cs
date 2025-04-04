using ProtoBuf;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractConnectPacket : AbstractGamePacket, IConnectPacket
{
    protected AbstractConnectPacket(string playerId) : base(playerId)
    {
    }
}