using System.Reflection;
using veloce.shared.handlers;
using veloce.shared.packets;
using veloce.tests.packets;
using veloce.tests.Tests;

namespace veloce.tests;

public static class Program
{
    public static void Main(string[] args)
    {
        PacketRegistry.FindAndLoadPackets(Assembly.GetExecutingAssembly());
        
        new HandshakeTest().Execute().Wait();
    }
}