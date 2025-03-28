namespace veloce.shared.models;

public abstract class AbstractServerConfig : IServerConfig
{
    public int TickRate { get; init; }
    public int TickInterval { get; init; }
    
    public int MaxWorkerCount { get; init; }
    
    public TimeSpan ClientTimeout { get; init; }
    public TimeSpan? ClientReconnectTimeout { get; init; }
    
    public int ProcessingThreshold { get; init; }

    protected AbstractServerConfig(int tickRate)
    {
        TickRate = tickRate;
        TickInterval = 1000 / tickRate;
        
        MaxWorkerCount = Environment.ProcessorCount;
        
        ClientTimeout = TimeSpan.FromSeconds(20);

        ProcessingThreshold = 1000;
    }
}

public sealed class DefaultServerConfig : AbstractServerConfig
{
    public DefaultServerConfig() : base(60)
    {
        ClientReconnectTimeout = TimeSpan.FromSeconds(120);
    }
}