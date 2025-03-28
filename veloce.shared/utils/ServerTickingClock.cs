namespace veloce.shared.utils;

/// <summary>
/// Represents a unique object for packet serialization and deserialization.
/// </summary>
public sealed class ServerTickingClock : AbstractTickingClock
{
    public ServerTickingClock(int tickInterval, CancellationToken token) : base(tickInterval, token)
    {
    }
}