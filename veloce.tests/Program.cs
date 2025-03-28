using veloce.tests.Tests;

namespace veloce.tests;

public static class Program
{
    public static void Main(string[] args)
    {
        new PacketLostTest().Do().Wait();
    }
}