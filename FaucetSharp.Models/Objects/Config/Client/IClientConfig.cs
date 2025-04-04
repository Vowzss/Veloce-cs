namespace FaucetSharp.Models.Objects.Config.Client;

/// <summary>
///     Represents an object to configure a client channel.
/// </summary>
public interface IClientConfig : IChannelConfig
{
    /// <summary>
    ///     Represents the player unique identifier.
    /// </summary>
    public string PlayerId { get; }
    
    /// <summary>
    ///     Represents the duration before the client needs to reconnect to the server.
    /// </summary>
    /// <remarks>This value is set by default to <c>20 seconds</c>.</remarks>
    public TimeSpan MaxTimeout { get; }
    
    /// <summary>
    ///     Represents the rate at which the client must send a heartbeat to the server.
    /// </summary>
    /// <remarks>This value is set by default to <c>1500 ms</c>.</remarks>
    public TimeSpan HeartbeatRate { get; }
}