using System.Net;
using veloce.shared.enums;

namespace veloce.shared.models;

public abstract class AbstractServerSession : AbstractSession<IServerEncryption>, IServerSession
{
    public ClientStatus Status { get; set; }

    public long? LastSeenAt { get; set; }
    public long? DisconnectAt { get; set; }

    protected AbstractServerSession(IPEndPoint endPoint, string id) : base(endPoint, id)
    {
    }
    
    public bool IsHealthy()
    {
        return Status == ClientStatus.Connected;
    }
}