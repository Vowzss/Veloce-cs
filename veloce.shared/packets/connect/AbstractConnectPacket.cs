using ProtoBuf;
using veloce.shared.attributes;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractConnectPacket : AbstractGamePacket, IConnectPacket
{
    
}

[PacketIdentifier("veloce.pkt.connect")]
public sealed class VeloceConnectPacket : AbstractConnectPacket
{
    
}

[PacketIdentifier("veloce.pkt.connect2")]
public sealed class VeloceConnectPacket2 : AbstractConnectPacket
{
    
}