using ProtoBuf;

using FaucetSharp.Models.Packets;

namespace FaucetSharp.Tests.packets;

[ProtoContract]
public interface IPositionPacket : IPacket
{
    public float X { get; }
    public float Y { get; }
    public float Z { get; }
}