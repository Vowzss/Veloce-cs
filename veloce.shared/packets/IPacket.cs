namespace veloce.shared.packets;

/// <summary>
/// Represents a data object for network communication.
/// </summary>
public interface IPacket
{
    /// <summary>
    /// Represents the unique identifier for the packet.
    /// </summary>
    public string Key { get; }
    
    /// <summary>
    /// Represents the client who sent the packet.
    /// </summary>
    public string ClientIdentifier { get; }
    
    /// <summary>
    /// Represents the timestamp when the packet was sent.
    /// </summary>
    public long Timestamp { get; }
    
    /// <summary>
    /// Represents the signature to ensure reliability.
    /// </summary>
    public byte[] Signature { get; }
}