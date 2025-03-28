using veloce.shared.models;
using veloce.shared.packets;

namespace veloce.shared.handlers;

/// <summary>
/// Represents an object for packet deserialization.
/// </summary>
public interface IPacketDeserializer : IPacketHandler
{
    /// <summary>
    /// Method to transform a binary format into a packet.
    /// </summary>
    public IPacket Read(byte[] data, EncryptionContext? encryption);
}