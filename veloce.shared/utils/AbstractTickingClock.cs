using System.Diagnostics;
using veloce.shared.events;

namespace veloce.shared.utils;

public abstract class AbstractTickingClock : ITickingClock
{
    public event TickEvent OnTick;
    public event TickMissedEvent OnTickMissed;
    
    /// <summary>
    /// Represents the time interval at which the clock must tick.
    /// </summary>
    protected readonly int TickInterval;
    
    protected readonly CancellationToken Token;
    protected readonly Stopwatch Stopwatch;
    
    protected AbstractTickingClock(int tickInterval, CancellationToken token)
    {
        TickInterval = tickInterval;
        
        Token = token;
        Stopwatch = new Stopwatch();
    }
    
    public virtual async Task Tick()
    {
        Stopwatch.Start();
        var lastTickTime = Stopwatch.ElapsedMilliseconds;

        while (!Token.IsCancellationRequested)
        {
            var currentTime = Stopwatch.ElapsedMilliseconds;
            var elapsedTime = currentTime - lastTickTime;

            try
            {
                if (elapsedTime < TickInterval)
                {
                    // Tick was missed
                    var remainingTime = TickInterval - elapsedTime;
                    
                    // Determine weather tick was missed or in time but need re-sync
                    if (remainingTime < 0) OnTickMissed.Invoke(elapsedTime);
                    else await Task.Delay((int)remainingTime, Token);
                    
                    return;
                }
                
                lastTickTime = currentTime;
                OnTick.Invoke();
            }
            finally
            {
                await Task.Delay(1, Token);
            }
        }
    }
}