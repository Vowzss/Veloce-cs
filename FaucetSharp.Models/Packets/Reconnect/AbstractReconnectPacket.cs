using ProtoBuf;

namespace FaucetSharp.Models.Packets.Reconnect;

[ProtoContract]
public abstract class AbstractReconnectPacket : AbstractGamePacket, IReconnectPacket
{
    protected AbstractReconnectPacket(string playerId) : base(playerId)
    {
    }
}