using System.Net;
using FaucetSharp.Models.Objects.Encryption;

namespace FaucetSharp.Models.Objects.Session;

public abstract class AbstractSession<TEncryption> : ISession<TEncryption>
    where TEncryption : IEncryptionContext
{
    public IPEndPoint EndPoint { get; }
    public string Id { get; }

    public TEncryption Encryption { get; protected init; }
    
    protected AbstractSession(IPEndPoint endPoint, string id)
    {
        EndPoint = endPoint;
        Id = id;
    }
}