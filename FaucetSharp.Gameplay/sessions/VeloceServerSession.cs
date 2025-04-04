using System.Net;
using FaucetSharp.Shared.models;

namespace FaucetSharp.Gameplay.encryption;

public sealed class FaucetServerSession : AbstractServerSession
{
    public FaucetServerSession(IPEndPoint endPoint, string id) : base(endPoint, id)
    {
        Encryption = new FaucetServerEncryption();
    }
}