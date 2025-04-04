using ProtoBuf;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractDisconnectPacket : AbstractGamePacket, IDisconnectPacket
{
    protected AbstractDisconnectPacket(string playerId) : base(playerId)
    {
    }
}