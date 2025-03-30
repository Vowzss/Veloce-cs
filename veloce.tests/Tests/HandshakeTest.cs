using System.Security.Cryptography;
using veloce.tests.packets;

namespace veloce.tests.Tests;

public sealed class HandshakeTest : AbstractTest
{
    private readonly RSA _rsa = RSA.Create();
    
    public override async Task Execute()
    {
        Client.Send(new FirstHandshakePacket {
            PublicKey = _rsa.ExportPkcs8PrivateKey()
        });
        
        Stop();
    }
}