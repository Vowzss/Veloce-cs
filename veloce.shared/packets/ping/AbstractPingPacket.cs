using ProtoBuf;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractPingPacket : AbstractGamePacket, IPingPacket
{
    
}