namespace FaucetSharp.Shared.models;

/// <summary>
///     Represents an object to configure a client channel.
/// </summary>
public interface IClientConfig : IChannelConfig
{
    /// <summary>
    ///     Represents the player unique identifier.
    /// </summary>
    public Guid PlayerId { get; }
}