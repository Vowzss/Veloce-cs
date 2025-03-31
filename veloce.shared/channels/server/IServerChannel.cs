using System.Net;
using veloce.shared.enums;
using veloce.shared.events;
using veloce.shared.handlers;
using veloce.shared.interceptors.server;
using veloce.shared.models;
using veloce.shared.packets;

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
    protected event TickEvent OnTick;

    /// <summary>
    /// Event fired whenever the server missed a tick.
    /// </summary>
    protected event TickMissedEvent OnTickMissed;
    
    protected void Start();
    protected void Stop();

    /// <summary>
    /// Non-blocking method to listen for incoming data.
    /// </summary>
    protected Task Listen();
    
    /// <summary>
    /// Method to send a packet to a connected client.
    /// </summary>
    /// <remarks>
    /// <para>This method resolves and verifies the validity of the client using the specified session id.</para>
    /// <para>Internally calls <see cref="Send(IServerSession, IPacket)"/>.</para>
    /// </remarks>
    protected Task Send(string sessionId, IPacket packet);
    
    /// <summary>
    /// Method to send a packet to a client.
    /// </summary>
    /// <remarks>This method should only be used when session was acquired beforehand and was deemed valid.</remarks>
    protected Task Send(IServerSession session, IPacket packet);
    
    /// <summary>
    /// Method to send a packet to all connected client.
    /// </summary>
    protected Task Broadcast(IPacket packet);
}