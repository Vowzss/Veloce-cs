using ProtoBuf;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractReconnectPacket : AbstractGamePacket, IReconnectPacket
{
    protected AbstractReconnectPacket(string playerId) : base(playerId)
    {
    }
}