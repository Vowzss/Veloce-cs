using System.Net;

namespace veloce.shared.models;

public abstract class AbstractServerSession : AbstractSession, IServerSession
{
    protected AbstractServerSession(IPEndPoint endPoint, EncryptionContext encryption) : base(endPoint, encryption)
    {
    }
}