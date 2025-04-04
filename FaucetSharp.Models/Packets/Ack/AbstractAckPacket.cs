using ProtoBuf;

namespace FaucetSharp.Models.Packets;

[ProtoContract]
public abstract class AbstractAckPacket : AbstractPacket, IAckPacket
{
    [ProtoMember(3)] 
    public bool IsSuccessful { get; }
    
    protected AbstractAckPacket(bool isSuccessful)
    {
        IsSuccessful = isSuccessful;
    }
}