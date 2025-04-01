using System.Net;

namespace veloce.shared.models;

public abstract class AbstractServerSession : AbstractSession, IServerSession
{
    protected AbstractServerSession(IPEndPoint endPoint, string id) : base(endPoint, id)
    {
    }
}

public sealed class VeloceServerSession : AbstractServerSession
{
    public VeloceServerSession(IPEndPoint endPoint, string id) : base(endPoint, id)
    {
    }
}