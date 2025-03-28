using System.Reflection;
using ProtoBuf;
using veloce.shared.attributes;
using veloce.shared.extensions;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractPacket : IPacket
{
    [ProtoMember(1)] 
    public string Key { get; init; } = null!;
    
    [ProtoMember(2)]
    public string ClientIdentifier { get; init; } = null!;
    
    [ProtoMember(3)]
    public long Timestamp { get; init; }

    [ProtoMember(4)]
    public byte[] Signature { get; init; }

    // Protobuf serialization
    protected AbstractPacket() { }

    protected AbstractPacket(string playerId)
    {
        var attribute  = GetType().GetCustomAttribute<PacketIdentifierAttribute>(); 
        if (attribute  == null) throw new InvalidOperationException($"Packet decoration is missing on {GetType()}.");

        Key = attribute.Key.ToUpperInvariant();
        ClientIdentifier = playerId;
        Timestamp = DateTimeExtensions.Now;
    }

    public override string ToString()
    {
        return $"Key:[{Key}] - Player:[{ClientIdentifier}] - Timestamp:[{Timestamp}]";
    }
}