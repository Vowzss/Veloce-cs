using FaucetSharp.Models.Objects.Encryption;
using FaucetSharp.Models.Packets;

namespace FaucetSharp.Core.Handlers;

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