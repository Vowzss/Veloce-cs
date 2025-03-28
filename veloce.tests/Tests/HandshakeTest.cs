using veloce.tests.packets;

namespace veloce.tests.Tests;

public sealed class HandshakeTest : AbstractTest
{
    public override async Task Execute()
    {
        Client.Send(new FirstHandshakePacket());
        
        Stop();
    }
}