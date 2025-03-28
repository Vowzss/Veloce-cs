using veloce.shared.events.server;
using veloce.shared.utils;

namespace veloce.shared.interceptors.server;

/// <summary>
/// Represents a packet interceptor object designed for server communication.
/// </summary>
public interface IServerPacketInterceptor : IPacketInterceptor
{
    /// <summary>
    /// Event fired whenever a client connects.
    /// </summary>
    protected ConnectEvent? OnConnect { get; }
    
    /// <summary>
    /// Event fired whenever the client disconnects.
    /// </summary>
    protected DisconnectEvent? OnDisconnect { get; }
    
    /// <summary>
    /// Event fired whenever the client reconnects.
    /// </summary>
    protected ReconnectEvent? OnReconnect { get; }
    
    /// <summary>
    /// Event fired whenever the client pongs.
    /// </summary>
    protected PongEvent? OnPong { get; }
}