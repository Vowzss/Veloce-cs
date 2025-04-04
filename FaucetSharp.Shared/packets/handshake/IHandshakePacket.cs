using FaucetSharp.Shared.enums;
using ProtoBuf;

namespace FaucetSharp.Shared.packets;

[ProtoContract]
public interface IHandshakePacket : IPacket
{
    public byte[]? Key { get; }

    public HandshakeStep Step { get; }
}