using System.Security.Cryptography;

namespace FaucetSharp.Models.Objects.Config.Server;

public abstract class AbstractServerConfig : AbstractChannelConfig, IServerConfig
{
    public RSA Rsa { get; }
    
    public int TickRate { get; protected init; }
    
    public TimeSpan MaxTimeout { get; protected init; }
    public TimeSpan? MaxReconnectTimeout { get; protected init; }
    
    protected AbstractServerConfig(int tickRate)
    {
        Rsa = RSA.Create();
        
        TickRate = tickRate;
        
        MaxTimeout = TimeSpan.FromSeconds(20);
    }
}