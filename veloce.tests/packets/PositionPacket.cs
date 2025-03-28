using ProtoBuf;
using veloce.shared.attributes;
using veloce.shared.extensions;
using veloce.shared.packets;

namespace veloce.tests.packets;

[ProtoContract]
public abstract class AbstractPositionPacket : AbstractPacket, IPositionPacket
{
    [ProtoMember(4)]
    public float X { get; init; }
    
    [ProtoMember(5)]
    public float Y { get; init; }
    
    [ProtoMember(6)]
    public float Z { get; init; }
    
    // Protobuf serialization
    protected AbstractPositionPacket() : base() { }
    
    
    
    public override string ToString()
    {
        return $"{base.ToString()} - Position: [X:{X}, Y:{Y}, Z:{Z}]";
    }
}

[ProtoContract]
[PacketIdentifier("veloce.demo.pkt.position")]
public sealed class PositionPacket : AbstractPositionPacket
{
    // Protobuf serialization
    private PositionPacket() : base() { }
    
    public PositionPacket(string playerId, float x, float y, float z)
    {
        ClientIdentifier = playerId;
        
        X = x;
        Y = y;
        Z = z;
    }
    
    public PositionPacket(string playerId, float[] position)
    {
        if (position is not { Length: 3 })
            throw new ArgumentException("Array must contain exactly 3 elements (X, Y, Z).");
        
        ClientIdentifier = playerId;
        
        X = position[0];
        Y = position[1];
        Z = position[2];
    }
}