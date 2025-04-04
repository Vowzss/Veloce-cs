namespace FaucetSharp.Models.Objects.Config.Client;

public abstract class AbstractClientConfig : AbstractChannelConfig, IClientConfig
{
    public Guid PlayerId { get; }
    
    protected AbstractClientConfig(Guid id)
    {
        PlayerId = id;
    }
}