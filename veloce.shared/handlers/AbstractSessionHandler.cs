using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Serilog;
using veloce.shared.events;
using veloce.shared.extensions;
using veloce.shared.models;
using veloce.shared.packets;

namespace veloce.shared.handlers;

public abstract class AbstractSessionHandler<TSession> : ISessionHandler<TSession>
    where TSession : ISession
{
    private const int PingInterval = 5000;
    private readonly Stopwatch _sw = new();
    
    public ConcurrentDictionary<IPEndPoint, TSession> Sessions { get; } = [];
    
    public ConnectEvent? OnConnect { get; set; }
    public DisconnectEvent? OnDisconnect { get; set;}
    public ReconnectEvent? OnReconnect { get; set; }
    public TimeoutEvent? OnTimeout { get; set; }
    
    protected AbstractSessionHandler()
    {
        _sw.Start();
    }

    public void Handle(UdpReceiveResult rs)
    {
        if (Sessions.TryGetValue(rs.RemoteEndPoint, out var session))
        {
            session.LastSeenAt = DateTimeExtensions.Now;
            return;
        }
        
        session = CreateSession(rs);
        Sessions[rs.RemoteEndPoint] = session;
        
        Log.Information($"New client connected: {rs.RemoteEndPoint}");
        OnConnect?.Invoke(rs);
    }

    public abstract TSession CreateSession(UdpReceiveResult rs);
}