using FaucetSharp.Core.Channels;
using FaucetSharp.Core.Handlers.Server;
using FaucetSharp.Core.interceptors.server;
using FaucetSharp.Models.Enums;
using FaucetSharp.Models.Events;
using FaucetSharp.Models.Objects.Clock;
using FaucetSharp.Models.Objects.Config.Server;
using FaucetSharp.Models.Objects.Session.Server;
using FaucetSharp.Models.Packets;

namespace FaucetSharp.Core.Channels.Server;

/// <summary>
///     Represents a channel object designed for server communication.
/// </summary>
public interface IServerChannel : IChannel<IServerConfig, IServerPacketInterceptor>
{
    /// <summary>
    ///     Represents the object for game's logical state.
    /// </summary>
    protected ServerState State { get; }

    /// <summary>
    ///     Represents the object for server's operational status.
    /// </summary>
    protected ServerStatus Status { get; }

    /// <summary>
    ///     Represents the object for client session handler.
    /// </summary>
    protected IServerSessionHandler SessionHandler { get; }

    /// <summary>
    ///     Represents the object for server's ticking behaviour.
    /// </summary>
    protected ITickingClock Clock { get; }
    
    /// <summary>
    ///     Event fired whenever the server ticks.
    /// </summary>
    protected event TickEvent OnTick;

    /// <summary>
    ///     Event fired whenever the server missed a tick.
    /// </summary>
    protected event TickMissedEvent OnTickMissed;

    /// <summary>
    ///     Non-blocking method to start the server.
    /// </summary>
    public Task Start();

    /// <summary>
    ///     Non-blocking method to stop the server.
    /// </summary>
    public Task Stop();

    /// <summary>
    ///     Method to send a packet to a connected client.
    /// </summary>
    /// <remarks>This method should only be used when session was acquired beforehand and was deemed valid.</remarks>
    protected Task Send(IServerSession session, IPacket packet);

    /// <summary>
    ///     Method to send a packet to all connected client.
    /// </summary>
    protected Task Broadcast(IPacket packet);
}