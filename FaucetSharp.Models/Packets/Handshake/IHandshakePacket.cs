using FaucetSharp.Models.Enums;
using ProtoBuf;

namespace FaucetSharp.Models.Packets.Handshake;

[ProtoContract]
public interface IHandshakePacket : IPacket
{
    public byte[]? Key { get; }

    public HandshakeStep Step { get; }
}