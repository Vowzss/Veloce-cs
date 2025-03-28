using veloce.shared.handlers;
using veloce.shared.packets;
using veloce.shared.utils;
using veloce.tests.packets;
using veloce.tests.Tests;

namespace veloce.tests;

public static class Program
{
    public static void Main(string[] args)
    {
        AbstractPacketHandler.RegisterPacketType<AbstractGamePacket, AbstractPositionPacket>();
        AbstractPacketHandler.RegisterPacketType<AbstractPositionPacket, PositionPacket>();
        
        new PacketLostTest().Execute().Wait();
    }
}