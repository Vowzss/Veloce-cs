using ProtoBuf;
using veloce.shared.attributes;
using veloce.shared.extensions;
using veloce.shared.packets;

namespace veloce.tests.packets;

[ProtoContract]
[PacketIdentifier("veloce.demo.pkt.connect")]
public class ConnectPacket : AbstractConnectPacket
{
    [ProtoMember(4)] 
    public string SessionIdentifier { get; init; } = null!;
    
    // Protobuf serialization
    private ConnectPacket() : base() { }
    
    public ConnectPacket(string playerId, string sessionId)
    {
        SessionIdentifier = sessionId;
    }
}