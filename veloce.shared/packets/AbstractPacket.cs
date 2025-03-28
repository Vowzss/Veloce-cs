using System.Reflection;
using ProtoBuf;
using veloce.shared.attributes;
using veloce.shared.extensions;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractPacket : IPacket
{
    [ProtoMember(1)] 
    public string Key { get; init; }
    
    [ProtoMember(2)]
    public long Timestamp { get; init; }
    
    // Protobuf serialization
    protected AbstractPacket()
    {
        var attribute  = GetType().GetCustomAttribute<PacketIdentifierAttribute>(); 
        if (attribute  == null) throw new InvalidOperationException($"Packet decoration is missing on {GetType()}.");

        Key = attribute.Key.ToUpperInvariant();
        Timestamp = DateTimeExtensions.Now;
    }
}