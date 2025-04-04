using System.Net;
using FaucetSharp.Core.Handlers.Server;
using FaucetSharp.Gameplay.Sessions;
using FaucetSharp.Models.Objects.Session.Server;
using FaucetSharp.Shared.Exceptions;

namespace FaucetSharp.Gameplay.Handlers;

public sealed class FaucetServerSessionHandler : AbstractServerSessionHandler
{
    public override IServerSession? Register(IPEndPoint endpoint)
    {
        var session = new FaucetServerSession(endpoint, ComputeId(endpoint));
        if (!Sessions.TryAdd(session.Id, session)) throw new SessionCreationException();
        return session;
    }

    public override string ComputeId(IPEndPoint endpoint)
    {
        return endpoint.ToString() ;
    }
}