using System.Net;
using veloce.gameplay.encryption;
using veloce.shared.exceptions;
using veloce.shared.handlers;
using veloce.shared.models;

namespace veloce.gameplay.handlers;

public sealed class VeloceServerSessionHandler : AbstractServerSessionHandler
{
    public override IServerSession? Register(IPEndPoint endpoint)
    {
        var session = new VeloceServerSession(endpoint, ComputeId(endpoint));
        if (!Sessions.TryAdd(session.Id, session)) throw new SessionCreationException();
        return session;
    }

    public override string ComputeId(IPEndPoint endpoint)
    {
        return endpoint.ToString() ;
    }
}