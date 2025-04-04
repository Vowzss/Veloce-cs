using FaucetSharp.Shared.enums;
using FaucetSharp.Shared.packets;

namespace FaucetSharp.Tests.Tests;

public sealed class HandshakeTest : AbstractTest
{
    public override async Task Execute()
    {
        await Stop();
    }
}