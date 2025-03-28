using veloce.shared.events;
using veloce.shared.models;
using veloce.shared.packets;
using veloce.shared.utils;

namespace veloce.shared.interceptors;

/// <summary>
/// Represents an object to handle packets received from transport.
/// </summary>
public interface IPacketInterceptor
{
    /// <summary>
    /// Represents the object for packet deserialization
    /// </summary>
    protected IPacketDeserializer Deserializer { get; }
    
    /// <summary>
    /// Event fired whenever the first handshake was received.
    /// </summary>
    protected FirstHandshakeEvent? OnFirstHandshake { get; }
    
    /// <summary>
    /// Event fired whenever the second handshake was received.
    /// </summary>
    protected SecondHandshakeEvent? OnSecondHandshake { get; }
    
    /// <summary>
    /// Non-blocking method to resolve packet from data.
    /// </summary>
    public void Accept(byte[] data, EncryptionContext encryption);
    
    /// <summary>
    /// Non-blocking method to process custom packets.
    /// </summary>
    public void Handle(IPacket packet);
}