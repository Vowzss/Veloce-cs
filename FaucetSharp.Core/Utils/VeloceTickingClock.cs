using FaucetSharp.Models.Objects.Clock;

namespace FaucetSharp.Core.Utils;

/// <summary>
///     Represents a unique object for packet serialization and deserialization.
/// </summary>
public sealed class FaucetTickingClock : AbstractTickingClock
{
    public FaucetTickingClock(int tickRate, CancellationToken token) : base(tickRate, token)
    {
    }
}