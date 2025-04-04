using veloce.shared.events;
using veloce.shared.events.client;
using veloce.shared.handlers;
using veloce.shared.models;

namespace veloce.shared.interceptors;

/// <summary>
///     Represents an object to handle packets received from transport.
/// </summary>
public interface IPacketInterceptor
{
    /// <summary>
    ///     Represents the object for packet deserialization
    /// </summary>
    protected IPacketDeserializer Deserializer { get; }

    /// <summary>
    ///     Event fired whenever a handshake was received.
    /// </summary>
    public event HandshakeEvent OnHandshake;

    /// <summary>
    ///     Event fired whenever a heartbeat was received.
    /// </summary>
    public event HeartbeatEvent OnHeartbeat;

    /// <summary>
    ///     Non-blocking method to resolve packet from data.
    /// </summary>
    protected internal void Accept(DataReceiveArgs args, IEncryptionContext encryption);

    /// <summary>
    ///     Non-blocking method to process custom packets.
    /// </summary>
    protected void Handle(IEventPacketArgs args);
}