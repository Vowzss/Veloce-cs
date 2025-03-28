using veloce.shared.models;
using veloce.shared.packets;

namespace veloce.shared.utils;

/// <summary>
/// Represents an object for packet serialization
/// </summary>
public interface IPacketSerializer : IPacketHandler
{
    /// <summary>
    /// Method to transform a packet into a binary format.
    /// </summary>
    public byte[] Write<TPacket>(TPacket packet, EncryptionContext encryption) where TPacket : class, IPacket;
}