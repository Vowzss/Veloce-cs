using System.Net;
using Serilog;
using veloce.shared.channels.client;
using veloce.shared.channels.server;
using veloce.shared.handlers;
using veloce.shared.interceptors.client;
using veloce.shared.interceptors.server;
using veloce.shared.models;
using veloce.shared.packets;
using veloce.shared.utils;

namespace veloce.tests.Tests;

public static class GameUpdateRates
{
    public const int Fps = 16;         // ~60 Hz (16ms per update)
    public const int BattleRoyal = 33; // ~30 Hz (33ms per update)
    public const int Moba = 50;        // ~20 Hz (50ms per update)
    public const int Mmorpg = 100;     // ~10 Hz (100ms per update)
    public const int Racing = 20;      // ~50 Hz (20ms per update)
}

public abstract class AbstractTest
{
    protected static readonly Random Random = new Random();
    
    protected readonly ILogger Logger;
    private readonly AbstractServerChannel _server;
    protected readonly AbstractClientChannel Client;

    protected AbstractTest()
    {
        var endpoint = new IPEndPoint(IPAddress.Loopback, 52512);
        var config = new DefaultServerConfig();
        
        AbstractPacketHandler.RegisterPacketType<AbstractPacket, AbstractFirstHandshakePacket>();
        AbstractPacketHandler.RegisterPacketType<AbstractPacket, AbstractSecondHandshakePacket>();
        
        AbstractPacketHandler.RegisterPacketType<AbstractPacket, AbstractGamePacket>();
        AbstractPacketHandler.RegisterPacketType<AbstractGamePacket, AbstractConnectPacket>();
        AbstractPacketHandler.RegisterPacketType<AbstractGamePacket, AbstractDisconnectPacket>();
        AbstractPacketHandler.RegisterPacketType<AbstractGamePacket, AbstractReconnectPacket>();
        AbstractPacketHandler.RegisterPacketType<AbstractGamePacket, AbstractPingPacket>();
        AbstractPacketHandler.RegisterPacketType<AbstractGamePacket, AbstractPongPacket>();
        
        Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss.fffd} {Level:u3}] > {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        
        // Create a server
        _server = new DefaultServerChannel(endpoint, config)
        {
            Serializer = new DefaultPacketSerializer(),
            PacketInterceptor = new DefaultServerPacketInterceptor(new DefaultPacketDeserializer()),
            SessionHandler = new DefaultServerSessionHandler(),
        };
        _server.Start();
        
        // Create a client
        Client = new DefaultClientChannel(endpoint)
        {
            Serializer = new DefaultPacketSerializer(),
            PacketInterceptor = new DefaultClientPacketInterceptor(new DefaultPacketDeserializer())
        };
    }

    public abstract Task Execute();

    protected void Stop()
    {
        Thread.Sleep(2000);
        _server.Stop();
        Thread.Sleep(2000);
    }
}