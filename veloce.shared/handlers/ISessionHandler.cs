using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using veloce.shared.events;
using veloce.shared.models;

namespace veloce.shared.handlers;

public interface ISessionHandler<TSession>
    where TSession : ISession
{
    public ConcurrentDictionary<IPEndPoint, TSession> Sessions { get; }

    public ConnectEvent? OnConnect { get; }
    public DisconnectEvent? OnDisconnect { get; }
    public ReconnectEvent? OnReconnect { get; }

    /// <summary>
    /// This method will execute user state logic.
    /// <para>e.g. state modifications, registering, unregistering, ...</para>
    /// </summary>
    public void Handle(UdpReceiveResult rs);

    /// <summary>
    /// This method will create a session.
    /// </summary>
    public TSession CreateSession(UdpReceiveResult rs);
}