
using FaucetSharp.Core.Interceptors;
using FaucetSharp.Models.Events;

namespace FaucetSharp.Core.interceptors.server;

/// <summary>
///     Represents a packet interceptor object designed for server communication.
/// </summary>
public interface IServerPacketInterceptor : IPacketInterceptor
{
    /// <summary>
    ///     Event fired whenever a client connects.
    /// </summary>
    public event ConnectEvent OnConnect;

    /// <summary>
    ///     Event fired whenever the client disconnects.
    /// </summary>
    public event DisconnectEvent OnDisconnect;

    /// <summary>
    ///     Event fired whenever the client reconnects.
    /// </summary>
    public event ReconnectEvent OnReconnect;
}