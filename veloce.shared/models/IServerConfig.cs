using System.Security.Cryptography;

namespace veloce.shared.models;

/// <summary>
/// Represents an object to configure a server channel.
/// </summary>
public interface IServerConfig
{
    /// <summary>
    /// Represents the server's target tick rate <c>in Hz</c>.
    /// </summary>
    public int TickRate { get; }
    
    /// <summary>
    /// Represents the amount of concurrent tasks that can process incoming packets.
    /// </summary>
    /// <remarks>This value is set by default to <see cref="Environment.ProcessorCount"/>.</remarks>
    public int MaxWorkerCount { get; }
    
    /// <summary>
    /// Represents the duration before the client is flagged as disconnected.
    /// </summary>
    /// <remarks>This value is set by default to <c>20 seconds</c>.</remarks>
    public TimeSpan ClientTimeout { get; }
    
    /// <summary>
    /// Represents the maximum duration before the client can reconnect.
    /// </summary>
    /// <remarks>Setting the value to <c>null</c> means the client can reconnect at any time.</remarks>
    public TimeSpan? ClientReconnectTimeout { get; }
    
    /// <summary>
    /// Represents the threshold before emitting a warning in case tasks are accumulating too much.
    /// </summary>
    /// <remarks>This value is set by default to <c>1000</c>.</remarks>
    public int ProcessingThreshold { get; }
    
    /// <summary>
    /// Represents the server's rsa instance.
    /// </summary>
    public RSA Rsa { get; }
    
    /// <summary>
    /// Represents the server's rsa public key.
    /// </summary>
    public byte[] GetPublicKey() => Rsa.ExportRSAPublicKey();
    
    /// <summary>
    /// Represents the server's rsa private key.
    /// </summary>
    public byte[] GetPrivateKey() => Rsa.ExportRSAPrivateKey();
}