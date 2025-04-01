using System.Security.Cryptography;

namespace veloce.shared.models;

public abstract class AbstractServerConfig : IServerConfig
{
    public int TickRate { get; init; }
    
    public int MaxWorkerCount { get; init; }
    
    public TimeSpan ClientTimeout { get; init; }
    public TimeSpan? ClientReconnectTimeout { get; init; }
    
    public int ProcessingThreshold { get; init; }
    
    public RSA Rsa { get; }

    protected AbstractServerConfig(int tickRate, string? privateKeyPem = null)
    {
        TickRate = tickRate;
        MaxWorkerCount = Environment.ProcessorCount;
        
        ClientTimeout = TimeSpan.FromSeconds(20);

        ProcessingThreshold = 1000;
        
        Rsa = RSA.Create();
        if (privateKeyPem != null) 
            Rsa.ImportFromPem(privateKeyPem);
    }
}

public sealed class VeloceServerConfig : AbstractServerConfig
{
    public VeloceServerConfig() : base(60)
    {
        ClientReconnectTimeout = TimeSpan.FromSeconds(120);
    }
}