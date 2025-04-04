using System.Reflection;
using FaucetSharp.Shared.attributes;
using FaucetSharp.Shared.extensions;
using ProtoBuf;

namespace FaucetSharp.Shared.packets;

[ProtoContract]
public abstract class AbstractPacket : IPacket
{
    [ProtoMember(1)] 
    public string Identifier { get; init; }

    [ProtoMember(2)] 
    public long Timestamp { get; init; }
    
    // Protobuf serialization
    protected AbstractPacket()
    {
        var attribute = GetType().GetCustomAttribute<PacketIdentifierAttribute>();
        if (attribute == null) throw new InvalidOperationException($"Packet decoration is missing on {GetType()}.");

        Identifier = attribute.Id.ToUpperInvariant();
        Timestamp = DateTimeExtensions.NowMs;
    }
    
    public override string ToString()
    {
        return $"Identifier:[{Identifier}] - Timestamp:[{Timestamp}]";
    }
}