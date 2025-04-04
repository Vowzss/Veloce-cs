using veloce.tests.packets;

namespace veloce.tests.Tests;

public sealed class PacketLostTest : AbstractTest
{
    // Simulation constraints
    private const int PacketsCounter = 50;
    private const float LossRate = 0.2f; // from 0 to 1
    private const int PacketDelayMax = 500; // in ms

    // Testing data
    private readonly float[] _position = [123.4f, 567.8f, 90.1f];
    private int _packetsDelayedCount;

    // Simulation data
    private int _packetsDroppedCount;

    public override async Task Execute()
    {
        var packetQueue = new List<(PositionPacket Packet, int Delay)>();

        for (var i = 0; i < PacketsCounter; i++)
        {
            // Introduce dropping
            if (Random.NextDouble() <= LossRate)
            {
                _packetsDroppedCount++;
                continue;
            }

            // Introduce delay (network jitter and congestion)
            var delay = Random.Next(0, PacketDelayMax);
            if (delay > 0) _packetsDelayedCount++;

            // Create a packet and enqueue it with delay
            var positionPacket = new PositionPacket("Player1", _position);
            packetQueue.Add((positionPacket, delay));

            _position[0] += i * 3;
            _position[2] += i * 2;
        }

        packetQueue = packetQueue.OrderBy(_ => Random.Next()).ToList();
        var tasks = packetQueue.Select(async item =>
        {
            await Task.Delay(item.Delay);
            Client.Send(item.Packet);
        });
        await Task.WhenAll(tasks);

        Stop();
        PrintSimulation();
    }

    private void PrintSimulation()
    {
        var lostRatio = (float)_packetsDroppedCount / PacketsCounter * 100;
        var delayedRatio = (float)_packetsDelayedCount / PacketsCounter * 100;

        Logger.Information("==========================");
        Logger.Information($"Total packets sent: {PacketsCounter - _packetsDroppedCount}");
        Logger.Information($"Total packets lost: {_packetsDroppedCount}");
        Logger.Information($"Packet loss rate: {lostRatio}%");
        Logger.Information($"Packet delayed rate: {delayedRatio}%");
    }
}