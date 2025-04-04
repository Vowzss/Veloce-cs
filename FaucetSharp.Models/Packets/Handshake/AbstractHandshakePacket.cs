using FaucetSharp.Models.Enums;
using ProtoBuf;

namespace FaucetSharp.Models.Packets.Handshake;

[ProtoContract]
public abstract class AbstractHandshakePacket : AbstractPacket, IHandshakePacket
{
    [ProtoMember(3)] 
    public byte[]? Key { get; init; }

    [ProtoMember(4)] 
    public required HandshakeStep Step { get; init; }
    
    // Protobuf serialization
    protected AbstractHandshakePacket()
    {
    }

    protected AbstractHandshakePacket(HandshakeStep step)
    {
        Step = step;
    }

    public override string ToString()
    {
        return $"{base.ToString()} - Step:[{Step}]";
    }
}