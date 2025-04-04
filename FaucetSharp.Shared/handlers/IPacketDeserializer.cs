using FaucetSharp.Shared.models;
using FaucetSharp.Shared.packets;

namespace FaucetSharp.Shared.handlers;

/// <summary>
///     Represents an object for secure packet deserialization.
/// </summary>
public interface IPacketDeserializer
{
    /// <summary>
    ///     Method to transform a binary format into a packet.
    /// </summary>
    public IPacket Read(byte[] data, IEncryptionContext encryption);
}