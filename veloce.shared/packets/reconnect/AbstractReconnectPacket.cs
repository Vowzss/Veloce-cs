using ProtoBuf;
using veloce.shared.attributes;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractReconnectPacket : AbstractGamePacket, IReconnectPacket
{
    
}


[PacketIdentifier("veloce.pkt.reconnect")]
public sealed class VeloceReconnectPacket : AbstractReconnectPacket
{
    
}