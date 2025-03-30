using System.Security.Cryptography;
using veloce.shared.enums;
using veloce.shared.packets;
using veloce.tests.packets;

namespace veloce.tests.Tests;

public sealed class HandshakeTest : AbstractTest
{
    private readonly RSA _rsa = RSA.Create();
    
    public override async Task Execute()
    {
        Client.Send(new VeloceHandshakePacket
        {
            Key = _rsa.ExportPkcs8PrivateKey(),
            Step = HandshakeStep.PublicKey
        });
        
        Stop();
    }
}