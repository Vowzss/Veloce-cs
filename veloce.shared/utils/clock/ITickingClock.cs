using veloce.shared.events;

namespace veloce.shared.utils;

/// <summary>
///     Represents an object for ticking.
/// </summary>
public interface ITickingClock
{
    /// <summary>
    ///     Represents the target tick rate <c>in Hz</c>.
    /// </summary>
    protected int TickRate { get; }

    /// <summary>
    ///     Event fired whenever the clock ticks.
    /// </summary>
    public event TickEvent OnTick;

    /// <summary>
    ///     Event fired whenever the clock missed a tick.
    /// </summary>
    public event TickMissedEvent OnTickMissed;

    /// <summary>
    ///     Non-blocking method to start the clock;
    /// </summary>
    protected internal Task Tick();
}