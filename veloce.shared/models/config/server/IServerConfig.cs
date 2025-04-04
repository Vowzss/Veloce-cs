using System.Security.Cryptography;

namespace veloce.shared.models;

/// <summary>
///     Represents an object to configure a server channel.
/// </summary>
public interface IServerConfig : IChannelConfig
{
    /// <summary>
    ///     Represents the server's rsa instance for secure communication.
    /// </summary>
    public RSA Rsa { get; }
    
    /// <summary>
    ///     Represents the server's target tick rate <c>in Hz</c>.
    /// </summary>
    public int TickRate { get; }

    /// <summary>
    ///     Represents the duration before the client is flagged as disconnected.
    /// </summary>
    /// <remarks>This value is set by default to <c>20 seconds</c>.</remarks>
    public TimeSpan ClientTimeout { get; }

    /// <summary>
    ///     Represents the maximum duration before the client can reconnect.
    /// </summary>
    /// <remarks>Setting the value to <c>null</c> means the client can reconnect at any time.</remarks>
    public TimeSpan? ClientReconnectTimeout { get; }
}