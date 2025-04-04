using ProtoBuf;

namespace FaucetSharp.Models.Packets.Connect;

[ProtoContract]
public abstract class AbstractConnectPacket : AbstractGamePacket, IConnectPacket
{
    protected AbstractConnectPacket(string playerId) : base(playerId)
    {
    }
}