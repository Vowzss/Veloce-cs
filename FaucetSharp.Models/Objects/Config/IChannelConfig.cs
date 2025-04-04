namespace FaucetSharp.Models.Objects.Config;

/// <summary>
///     Represents an object to configure a channel.
/// </summary>
public interface IChannelConfig
{
    /// <summary>
    ///     Represents the amount of concurrent tasks that can process incoming packets.
    /// </summary>
    /// <remarks>This value is set by default to <see cref="Environment.ProcessorCount" />.</remarks>
    public int MaxWorkerCount { get; }
    
    /// <summary>
    ///     Represents the threshold before emitting a warning in case tasks are accumulating too much.
    /// </summary>
    /// <remarks>This value is set by default to <c>1000</c>.</remarks>
    public int MaxProcessThreshold { get; }
}