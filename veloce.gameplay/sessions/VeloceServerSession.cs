using System.Net;
using veloce.shared.models;

namespace veloce.gameplay.encryption;

public sealed class VeloceServerSession : AbstractServerSession
{
    public VeloceServerSession(IPEndPoint endPoint, string id) : base(endPoint, id)
    {
        Encryption = new VeloceServerEncryption();
    }
}