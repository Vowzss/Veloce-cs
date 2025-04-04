namespace veloce.shared.models;

public abstract class AbstractChannelConfig : IChannelConfig
{
    public int MaxWorkerCount { get; init; }
    public int MaxProcessThreshold { get; init; }
    
    protected AbstractChannelConfig()
    {
        MaxWorkerCount = Environment.ProcessorCount;
        MaxProcessThreshold = 1000;
    }
}