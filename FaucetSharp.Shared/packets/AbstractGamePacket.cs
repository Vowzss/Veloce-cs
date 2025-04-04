using ProtoBuf;

namespace FaucetSharp.Shared.packets;

[ProtoContract]
public abstract class AbstractGamePacket : AbstractPacket, IGamePacket
{
    [ProtoMember(3)] 
    public string PlayerId { get; init; }

    protected AbstractGamePacket(string playerId)
    {
        PlayerId = playerId;
    }

    public override string ToString()
    {
        return $"{base.ToString()} - Player:[{PlayerId}]";
    }
}