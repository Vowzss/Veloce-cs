using ProtoBuf;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractPongPacket : AbstractGamePacket, IPongPacket
{
    
}