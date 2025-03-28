using System.Collections.Concurrent;
using System.Net;
using veloce.shared.models;

namespace veloce.shared.handlers;

public abstract class AbstractSessionHandler<TSession> : ISessionHandler<TSession>
    where TSession : class, ISession
{
    public IDictionary<IPEndPoint, TSession> Sessions { get; }

    protected AbstractSessionHandler()
    {
        Sessions = new ConcurrentDictionary<IPEndPoint, TSession>();
    }
    
    public TSession? Get(IPEndPoint endpoint)
    {
        Sessions.TryGetValue(endpoint, out var session);
        return session;
    }
}