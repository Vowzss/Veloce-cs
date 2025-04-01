namespace veloce.shared.utils;

/// <summary>
/// Represents a unique object for packet serialization and deserialization.
/// </summary>
public sealed class ServerTickingClock : AbstractTickingClock
{
    public ServerTickingClock(int tickRate, CancellationToken token) : base(tickRate, token)
    {
    }
}