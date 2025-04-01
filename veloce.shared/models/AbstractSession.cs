using System.Net;
using veloce.shared.enums;
using veloce.shared.extensions;

namespace veloce.shared.models;

public abstract class AbstractSession : ISession
{
    public IPEndPoint EndPoint { get; }
    public string Id { get; }
    
    public ClientStatus Status { get; protected set; }
    public long? LastSeenAt { get; protected set; }
    public long? DisconnectAt { get; protected set; }
    
    public EncryptionContext Encryption { get; protected set; }

    protected AbstractSession(IPEndPoint endPoint, string id)
    {
        EndPoint = endPoint;
        Id = id;
        
        Status = ClientStatus.Unknown;
        Encryption = new EncryptionContext();
    }
}