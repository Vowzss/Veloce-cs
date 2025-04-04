using ProtoBuf;

namespace FaucetSharp.Shared.packets;

[ProtoContract]
public abstract class AbstractReconnectPacket : AbstractGamePacket, IReconnectPacket
{
    protected AbstractReconnectPacket(string playerId) : base(playerId)
    {
    }
}