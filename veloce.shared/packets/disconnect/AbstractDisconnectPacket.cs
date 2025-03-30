using ProtoBuf;
using veloce.shared.attributes;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractDisconnectPacket : AbstractGamePacket, IDisconnectPacket
{
    
}

[PacketIdentifier("veloce.pkt.disconnect")]
public sealed class VeloceDisconnectPacket : AbstractDisconnectPacket
{
    
}