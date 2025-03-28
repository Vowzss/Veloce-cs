using System.Net;
using Serilog;
using veloce.client;
using veloce.server;
using veloce.shared.models;
using veloce.tests.packets;

namespace veloce.tests.Tests;

public static class GameUpdateRates
{
    public const int Fps = 16;         // ~60 Hz (16ms per update)
    public const int BattleRoyal = 33; // ~30 Hz (33ms per update)
    public const int Moba = 50;        // ~20 Hz (50ms per update)
    public const int Mmorpg = 100;     // ~10 Hz (100ms per update)
    public const int Racing = 20;      // ~50 Hz (20ms per update)
}

public class PacketLostTest
{
    private static readonly Random Random = new Random();
    
    private readonly AbstractServer<DemoSession> _server;
    private readonly AbstractClient _client;
    
    // Simulation constraints
    private const int PacketsCounter = 50;
    private const float LossRate = 0.2f; // from 0 to 1
    private const int PacketDelayMax = 500; // in ms
    
    // Testing data
    private readonly float[] _position = [123.4f, 567.8f, 90.1f];
    
    // Simulation data
    private int _packetsDroppedCount = 0;
    private int _packetsDelayedCount = 0;
    
    public PacketLostTest()
    {
        var endpoint = new IPEndPoint(IPAddress.Loopback, 52512);
        var config = new ServerConfig();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss.ffffff} {Level:u3}] > {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        
        // Create a server
        _server = new AbstractServer<DemoSession>(endpoint, config) {
            PacketHandler = new ServerPacketInterceptor(),
            SessionHandler = new SessionHandler(),
        };
        _server.Start();
        
        // Create a client
        _client = new AbstractClient(endpoint) {
            PacketHandler = new ServerPacketInterceptor()
        };
    }
    
    public async Task Do()
    {
        var packetQueue = new List<(PositionPacket Packet, int Delay)>();
        
        for (int i = 0; i < PacketsCounter; i++)
        {
            // Introduce dropping
            if (Random.NextDouble() < LossRate)
            {
                _packetsDroppedCount++;
                continue;
            }

            // Introduce delay (network jitter and congestion)
            int delay = Random.Next(0, PacketDelayMax);
            if (delay > 0)
            {
                _packetsDelayedCount++;
            }

            // Create a packet and enqueue it with delay
            var positionPacket = new PositionPacket("Player1", _position);
            packetQueue.Add((positionPacket, delay));

            _position[0] += i * 3;
            _position[2] += i * 2;
        }
        
        packetQueue = packetQueue.OrderBy(_ => Random.Next()).ToList();
        var tasks = packetQueue.Select(async item => {
            await Task.Delay(item.Delay);
            await _client.Send(item.Packet);
        });
        await Task.WhenAll(tasks);
        
        Thread.Sleep(2000);
        _server.Stop();
        
        
    }

    private void PrintSimulation()
    {
        float lostRatio = (float)_packetsDroppedCount / PacketsCounter * 100;
        float delayedRatio = (float)_packetsDelayedCount / PacketsCounter * 100;
        
        Log.Information("==========================");
        Log.Information($"Total packets sent: {PacketsCounter - _packetsDroppedCount}");
        Log.Information($"Total packets lost: {_packetsDroppedCount}");
        Log.Information($"Packet loss rate: {lostRatio}%");
        Log.Information($"Packet delayed rate: {delayedRatio}%");
    }
}

