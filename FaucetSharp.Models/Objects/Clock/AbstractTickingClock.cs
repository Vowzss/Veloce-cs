using System.Diagnostics;
using FaucetSharp.Models.Events;

namespace FaucetSharp.Models.Objects.Clock;

public abstract class AbstractTickingClock : ITickingClock
{
    public int TickRate { get; }

    public event TickEvent OnTick;
    public event TickMissedEvent OnTickMissed;
    
    protected readonly int TickInterval;
    protected readonly CancellationToken Token;
    protected readonly Stopwatch Stopwatch;
    
    protected AbstractTickingClock(int tickRate, CancellationToken token)
    {
        TickRate = tickRate;

        TickInterval = 1000 / tickRate;
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