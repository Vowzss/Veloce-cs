using System.Net;
using FaucetSharp.Shared.models;

namespace FaucetSharp.Gameplay.encryption;

public sealed class VeloceServerSession : AbstractServerSession
{
    public VeloceServerSession(IPEndPoint endPoint, string id) : base(endPoint, id)
    {
        Encryption = new VeloceServerEncryption();
    }
}