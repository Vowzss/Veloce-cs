using System.Security.Cryptography;

namespace veloce.shared.models;

public abstract class AbstractServerConfig : AbstractChannelConfig, IServerConfig
{
    public RSA Rsa { get; }
    
    public int TickRate { get; protected init; }
    
    public TimeSpan ClientTimeout { get; protected init; }
    public TimeSpan? ClientReconnectTimeout { get; protected init; }
    
    protected AbstractServerConfig(int tickRate)
    {
        Rsa = RSA.Create();
        
        TickRate = tickRate;
        
        ClientTimeout = TimeSpan.FromSeconds(20);
    }
}