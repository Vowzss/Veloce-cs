using System.Collections.Concurrent;
using System.Net;
using FaucetSharp.Models.Objects.Session.Server;

namespace FaucetSharp.Core.Handlers.Server;

public abstract class AbstractServerSessionHandler : IServerSessionHandler
{
    protected AbstractServerSessionHandler()
    {
        Sessions = new ConcurrentDictionary<string, IServerSession>();
    }

    public IDictionary<string, IServerSession> Sessions { get; }

    public abstract IServerSession? Register(IPEndPoint endpoint);

    public abstract string ComputeId(IPEndPoint endpoint);

    public IServerSession? FindById(string sessionId)
    {
        Sessions.TryGetValue(sessionId, out var session);
        return session;
    }

    public IServerSession? FindByEndpoint(IPEndPoint endpoint)
    {
        return FindById(ComputeId(endpoint));
    }

    public IList<IServerSession> GetAll()
    {
        return Sessions.Values.ToList();
    }
}