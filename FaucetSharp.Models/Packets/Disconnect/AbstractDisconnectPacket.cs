using ProtoBuf;

namespace FaucetSharp.Models.Packets.Disconnect;

[ProtoContract]
public abstract class AbstractDisconnectPacket : AbstractGamePacket, IDisconnectPacket
{
    protected AbstractDisconnectPacket(string playerId) : base(playerId)
    {
    }
}