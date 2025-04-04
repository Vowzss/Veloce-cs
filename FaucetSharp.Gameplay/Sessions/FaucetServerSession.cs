using System.Net;
using FaucetSharp.Gameplay.Encryption;
using FaucetSharp.Models.Objects.Session.Server;

namespace FaucetSharp.Gameplay.Sessions;

public sealed class FaucetServerSession : AbstractServerSession
{
    public FaucetServerSession(IPEndPoint endPoint, string id) : base(endPoint, id)
    {
        Encryption = new FaucetServerEncryption();
    }
}