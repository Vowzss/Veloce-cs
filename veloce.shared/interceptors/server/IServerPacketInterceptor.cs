using veloce.shared.events;
using veloce.shared.events.client;

namespace veloce.shared.interceptors.server;

/// <summary>
/// Represents a packet interceptor object designed for server communication.
/// </summary>
public interface IServerPacketInterceptor : IPacketInterceptor
{
    /// <summary>
    /// Event fired whenever a client connects.
    /// </summary>
    protected internal event ConnectEvent OnConnect;

    /// <summary>
    /// Event fired whenever the client disconnects.
    /// </summary>
    protected internal event DisconnectEvent OnDisconnect;

    /// <summary>
    /// Event fired whenever the client reconnects.
    /// </summary>
    protected internal event ReconnectEvent OnReconnect;
}