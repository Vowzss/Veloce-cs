using ProtoBuf;
using veloce.shared.packets;

namespace veloce.tests.packets;

[ProtoContract]
public interface IPositionPacket : IPacket
{
    public float X { get;  }
    public float Y { get;  }
    public float Z { get; }
}