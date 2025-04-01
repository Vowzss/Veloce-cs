using System.Security.Cryptography;
using veloce.shared.enums;
using veloce.shared.models;
using veloce.shared.packets;
using veloce.tests.packets;

namespace veloce.tests.Tests;

public sealed class HandshakeTest : AbstractTest
{
    public override async Task Execute()
    {
        await Client.Send(new VeloceHandshakePacket
        {
            Step = HandshakeStep.PublicKey
        });
        
        Stop();
    }
}