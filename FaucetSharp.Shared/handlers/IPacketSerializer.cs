using FaucetSharp.Shared.models;
using FaucetSharp.Shared.packets;

namespace FaucetSharp.Shared.handlers;

/// <summary>
///     Represents an object for secure packet serialization.
/// </summary>
public interface IPacketSerializer
{
    /// <summary>
    ///     Method to transform a packet into a binary format.
    /// </summary>
    public byte[] Write<TPacket>(TPacket packet, IEncryptionContext encryption) where TPacket : class, IPacket;
}