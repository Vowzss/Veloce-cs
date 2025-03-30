using ProtoBuf;
using veloce.shared.attributes;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractGamePacket : AbstractPacket, IGamePacket
{
    [ProtoMember(3)]
    public string ClientIdentifier { get; init; } = null!;
    
    // Protobuf serialization
    protected AbstractGamePacket() { }

    protected AbstractGamePacket(string playerId) : base()
    {
        ClientIdentifier = playerId;
    }

    public override string ToString()
    {
        return $"Identifier:[{Identifier}] - Player:[{ClientIdentifier}] - Timestamp:[{Timestamp}]";
    }
}



[ProtoContract]
public abstract class AbstractFdpPacket : AbstractGamePacket
{
}

[ProtoContract]
public abstract class AbstractSafeFdpPacket : AbstractFdpPacket
{
}

[ProtoContract]
[PacketIdentifier("veloce.pkt.fdp")]
public sealed class FdpPacket : AbstractSafeFdpPacket
{
    
}