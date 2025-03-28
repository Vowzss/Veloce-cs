﻿namespace veloce.shared.packets;

/// <summary>
/// Represents a secure data object for game communication.
/// </summary>
public interface IGamePacket : IPacket
{
    /// <summary>
    /// Represents the client who sent the packet.
    /// </summary>
    public string ClientIdentifier { get; }
}