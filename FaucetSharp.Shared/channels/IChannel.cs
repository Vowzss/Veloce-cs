using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using FaucetSharp.Shared.handlers;
using FaucetSharp.Shared.interceptors;
using FaucetSharp.Shared.models;
using Serilog;

namespace FaucetSharp.Shared.channels;

/// <summary>
///     Represents a wrapper around the socket interface.
/// </summary>
public interface IChannel<out TChannelConfig, out TPacketInterceptor>
    where TChannelConfig : IChannelConfig
    where TPacketInterceptor : IPacketInterceptor
{
    /// <summary>
    ///     Represents a logger for monitoring purposes.
    /// </summary>
    protected ILogger Logger { get; }

    /// <summary>
    ///     Represents the object for configuration.
    /// </summary>
    protected TChannelConfig Config { get; }
    
    /// <summary>
    ///     Represents the endpoint to communicate to/from.
    /// </summary>
    /// <remarks>This must always be the server endpoint.</remarks>
    protected IPEndPoint EndPoint { get; }
    
    /// <summary>
    ///     Represents whether it's acting as a server or not.
    /// </summary>
    public bool HasAuthority { get; }
    
    /// <summary>
    ///     Represents the interceptor object for packet processing.
    /// </summary>
    protected TPacketInterceptor PacketInterceptor { get; }

    /// <summary>
    ///     Represents the object for packet serialization
    /// </summary>
    protected internal IPacketSerializer Serializer { get; }

    /// <summary>
    ///     Represents the object for packet deserialization
    /// </summary>
    protected IPacketDeserializer Deserializer { get; }
    
    /// <summary>
    ///     Represents the object for cancelling any threading tasks.
    /// </summary>
    protected CancellationTokenSource Signal { get; }
    
    /// <summary>
    ///     Represents the actual signal token stored for access utility.
    /// </summary>
    protected CancellationToken Token { get; }
    
    /// <summary>
    ///     Represents the object for holding incoming data that needs to be processed.
    /// </summary>
    protected ConcurrentQueue<UdpReceiveResult> Queue { get; }
    
    /// <summary>
    ///     Represents the object used to limit concurrent data processing.
    /// </summary>
    protected SemaphoreSlim Semaphore { get; }
    
    /// <summary>
    ///     Represents the object that process data concurrently.
    /// </summary>
    protected List<Task> Workers { get; }
    
    /// <summary>
    ///     Non-blocking method to listen for incoming data.
    /// </summary>
    protected Task Listen();

    /// <summary>
    ///     Non-blocking method to process received data.
    /// </summary>
    protected Task Process();

    /// <summary>
    ///     Utility method to check whether the channel needs to stop any activity.
    /// </summary>
    protected bool IsShuttingDown();
}