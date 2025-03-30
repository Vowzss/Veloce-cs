using veloce.shared.events;
using veloce.shared.events.client;
using veloce.shared.handlers;
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
    protected internal IPacketDeserializer Deserializer { get; }

    /// <summary>
    /// Event fired whenever a handshake was received.
    /// </summary>
    protected internal event HandshakeEvent OnHandshake;
    
    /// <summary>
    /// Event fired whenever a heartbeat was received.
    /// </summary>
    protected internal event HeartbeatEvent OnHeartbeat;
    
    /// <summary>
    /// Non-blocking method to resolve packet from data.
    /// </summary>
    protected internal void Accept(DataReceiveArgs args, EncryptionContext? encryption);
    
    /// <summary>
    /// Non-blocking method to process custom packets.
    /// </summary>
    protected internal void Handle(IEventPacketArgs args);
}