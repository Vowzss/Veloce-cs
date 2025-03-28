using ProtoBuf;

namespace veloce.shared.packets;

[ProtoContract]
public abstract class AbstractDisconnectPacket : AbstractGamePacket, IDisconnectPacket
{
    
}