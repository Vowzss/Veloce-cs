namespace FaucetSharp.Models.Objects.Config.Client;

public abstract class AbstractClientConfig : AbstractChannelConfig, IClientConfig
{
    public string PlayerId { get; }
    
    public TimeSpan MaxTimeout { get; protected init; }
    public TimeSpan HeartbeatRate { get; protected init; }

    protected AbstractClientConfig(string id)
    {
        PlayerId = id;
        
        HeartbeatRate = TimeSpan.FromMilliseconds(1500);
    }
}