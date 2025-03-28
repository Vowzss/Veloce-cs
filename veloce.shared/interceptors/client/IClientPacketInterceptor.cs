using veloce.shared.events.client;

namespace veloce.shared.interceptors.client;

/// <summary>
/// Represents a packet interceptor object designed for client communication.
/// </summary>
public interface IClientPacketInterceptor : IPacketInterceptor
{
    /// <summary>
    /// Event fired whenever the server pings.
    /// </summary>
    protected PingEvent? OnPing { get; }
}