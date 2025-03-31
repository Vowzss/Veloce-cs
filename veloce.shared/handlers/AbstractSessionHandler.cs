using System.Collections.Concurrent;
using System.Net;
using veloce.shared.models;

namespace veloce.shared.handlers;

public abstract class AbstractSessionHandler<TSession> : ISessionHandler<TSession>
    where TSession : class, ISession
{
    public IDictionary<string, TSession> Sessions { get; }

    protected AbstractSessionHandler()
    {
        Sessions = new ConcurrentDictionary<string, TSession>();
    }
    
    public TSession? Get(string sessionId)
    {
        Sessions.TryGetValue(sessionId, out var session);
        return session;
    }
}