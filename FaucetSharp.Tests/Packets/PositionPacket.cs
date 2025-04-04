using ProtoBuf;

using FaucetSharp.Models.Attributes;
using FaucetSharp.Models.Packets;

namespace FaucetSharp.Tests.packets;

[ProtoContract]
public abstract class AbstractPositionPacket : AbstractGamePacket, IPositionPacket
{
    [ProtoMember(4)] 
    public float X { get; init; }

    [ProtoMember(5)] 
    public float Y { get; init; }

    [ProtoMember(6)] 
    public float Z { get; init; }
    
    protected AbstractPositionPacket(string playerId) : base(playerId)
    {
    }
    
    public override string ToString()
    {
        return $"{base.ToString()} - Position: [X:{X}, Y:{Y}, Z:{Z}]";
    }
}

[ProtoContract]
[PacketIdentifier("faucet.demo.pkt.position")]
public sealed class PositionPacket : AbstractPositionPacket
{
    public PositionPacket(string playerId, float x, float y, float z) : base(playerId)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public PositionPacket(string playerId, float[] position) : base(playerId)
    {
        if (position is not { Length: 3 })
            throw new ArgumentException("Array must contain exactly 3 elements (X, Y, Z).");

        PlayerId = playerId;

        X = position[0];
        Y = position[1];
        Z = position[2];
    }
}