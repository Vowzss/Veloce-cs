using System.Reflection;
using FaucetSharp.Shared.handlers;
using FaucetSharp.Tests.Tests;

namespace FaucetSharp.Tests;

public static class Program
{
    public static void Main(string[] args)
    {
        PacketRegistry.FindAndLoadPackets(Assembly.GetExecutingAssembly());

        new HandshakeTest().Execute().Wait();
    }
}