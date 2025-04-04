using System.Net;

namespace veloce.shared.models;

public abstract class AbstractSession<TEncryption> : ISession<TEncryption>
    where TEncryption : IEncryptionContext
{
    protected AbstractSession(IPEndPoint endPoint, string id)
    {
        EndPoint = endPoint;
        Id = id;
    }

    public IPEndPoint EndPoint { get; }
    public string Id { get; }

    public TEncryption Encryption { get; protected set; }
}