using System.Collections.Concurrent;
using System.Net;
using veloce.shared.models;

namespace veloce.shared.handlers;

public abstract class AbstractServerSessionHandler : IServerSessionHandler
{
    public IDictionary<string, IServerSession> Sessions { get; }
    
    protected AbstractServerSessionHandler()
    {
        Sessions = new ConcurrentDictionary<string, IServerSession>();
    }
    
    public abstract IServerSession Register(IPEndPoint endpoint);
    
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

    public IList<IServerSession> GetAll() => Sessions.Values.ToList();
}

public sealed class DefaultServerSessionHandler : AbstractServerSessionHandler
{
    public override IServerSession Register(IPEndPoint endpoint)
    {
        return new VeloceServerSession(endpoint, ComputeId(endpoint));
    }

    public override string ComputeId(IPEndPoint endpoint)
    {
        return endpoint.ToString();
    }
}