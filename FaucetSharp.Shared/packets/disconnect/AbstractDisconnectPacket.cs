using ProtoBuf;

namespace FaucetSharp.Shared.packets;

[ProtoContract]
public abstract class AbstractDisconnectPacket : AbstractGamePacket, IDisconnectPacket
{
    protected AbstractDisconnectPacket(string playerId) : base(playerId)
    {
    }
}