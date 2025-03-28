using veloce.shared.enums;
using veloce.shared.events;
using veloce.shared.handlers;
using veloce.shared.interceptors.server;
using veloce.shared.models;

namespace veloce.shared.channels.server;

/// <summary>
/// Represents a channel object designed for server communication.
/// </summary>
public interface IServerChannel : IChannel<IServerPacketInterceptor>
{
    /// <summary>
    /// Represents the object for configuration.
    /// </summary>
    protected IServerConfig Config { get; }
    
    /// <summary>
    /// Represents the object for game's logical state.
    /// </summary>
    protected ServerState State { get; }
    
    /// <summary>
    /// Represents the object for server's operational status.
    /// </summary>
    protected ServerStatus Status { get; }
    
    /// <summary>
    /// Represents the object for client session handler.
    /// </summary>
    protected IServerSessionHandler SessionHandler { get; }
    
    /// <summary>
    /// Event fired whenever the server ticks.
    /// </summary>
    protected TickEvent? OnTick { get; }
    
    /// <summary>
    /// Event fired whenever the server missed a tick.
    /// </summary>
    protected TickMissedEvent? OnTickMissed { get; }
    
    protected void Start();
    protected void Stop();

    /// <summary>
    /// Non-blocking method to listen for incoming data.
    /// </summary>
    protected Task Listen();
}