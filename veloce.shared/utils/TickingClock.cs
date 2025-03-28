using System.Diagnostics;

namespace veloce.shared.utils;

public class TickingClock
{
    private int TickInterval { get; }
    
    public event Action OnTick;
    public event Action<long> OnTickMissed;
    
    private readonly CancellationToken _token;
    private readonly Stopwatch _stopwatch;
    
    public TickingClock(int tickInterval, CancellationToken token)
    {
        TickInterval = tickInterval;
        
        _token = token;
        _stopwatch = new Stopwatch();
    }
    
    public async Task Tick()
    {
        _stopwatch.Start();
        var lastTickTime = _stopwatch.ElapsedMilliseconds;

        while (!_token.IsCancellationRequested)
        {
            var currentTime = _stopwatch.ElapsedMilliseconds;
            var elapsedTime = currentTime - lastTickTime;

            try
            {
                if (elapsedTime < TickInterval)
                {
                    // Tick was missed
                    var remainingTime = TickInterval - elapsedTime;
                    
                    // Determine weather tick was missed or in time but need re-sync
                    if (remainingTime < 0) OnTickMissed?.Invoke(elapsedTime);
                    else await Task.Delay((int)remainingTime, _token);
                    
                    return;
                }
                
                lastTickTime = currentTime;
                OnTick?.Invoke();
            }
            finally
            {
                await Task.Delay(1, _token);
            }
        }
    }
}