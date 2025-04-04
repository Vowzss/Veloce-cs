using System.Security.Cryptography;

namespace FaucetSharp.Shared.models;

public abstract class AbstractClientConfig : AbstractChannelConfig, IClientConfig
{
    public Guid PlayerId { get; }
    
    protected AbstractClientConfig(Guid id)
    {
        PlayerId = id;
    }
}