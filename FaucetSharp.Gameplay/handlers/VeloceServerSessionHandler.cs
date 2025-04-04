using System.Net;
using FaucetSharp.Gameplay.encryption;
using FaucetSharp.Shared.exceptions;
using FaucetSharp.Shared.handlers;
using FaucetSharp.Shared.models;

namespace FaucetSharp.Gameplay.handlers;

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