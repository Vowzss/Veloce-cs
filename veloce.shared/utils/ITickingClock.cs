using System.Diagnostics;
using veloce.shared.events;

namespace veloce.shared.utils;

/// <summary>
/// Represents an object for ticking.
/// </summary>
public interface ITickingClock
{
    /// <summary>
    /// Event fired whenever the clock ticks.
    /// </summary>
    public TickEvent? OnTick { get; set; }

    /// <summary>
    /// Event fired whenever the clock missed a tick.
    /// </summary>
    public TickMissedEvent? OnTickMissed { get; set; }

    /// <summary>
    /// Non-blocking method to start the clock;
    /// </summary>
    public Task Tick();
}