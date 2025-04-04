using ProtoBuf;

namespace FaucetSharp.Shared.packets;

[ProtoContract]
public abstract class AbstractConnectPacket : AbstractGamePacket, IConnectPacket
{
    protected AbstractConnectPacket(string playerId) : base(playerId)
    {
    }
}