namespace veloce.shared.utils;

/// <summary>
///     Represents a unique object for packet serialization and deserialization.
/// </summary>
public sealed class VeloceTickingClock : AbstractTickingClock
{
    public VeloceTickingClock(int tickRate, CancellationToken token) : base(tickRate, token)
    {
    }
}