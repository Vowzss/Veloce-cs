using System.Net;
using Serilog;
using veloce.shared.events;
using veloce.shared.utils;

namespace veloce.shared.channels;

/// <summary>
/// Represents a wrapper around the socket interface.
/// </summary>
public interface IChannel
{
    /// <summary>
    /// Represents a logger for debug purposes.
    /// </summary>
    public ILogger Logger { get; }
    
    /// <summary>
    /// Represents whether it's acting as a server or not.
    /// </summary>
    public bool HasAuthority { get; }
    
    /// <summary>
    /// Represents the endpoint to communicate to/from.
    /// </summary>
    /// <remarks>This must always be the server endpoint.</remarks>
    public IPEndPoint EndPoint { get; }
    
    /// <summary>
    /// Represents a signal for cancelling any threading tasks.
    /// </summary>
    protected CancellationTokenSource Signal { get; }
    
    /// <summary>
    /// Represents the object for packet serialization
    /// </summary>
    protected IPacketSerializer Serializer { get; }
    
    /// <summary>
    /// Event fired when data is received from transport.
    /// </summary>
    /// <remarks>The data is always provided in <c>byte[]</c>.</remarks>
    public DataReceiveEvent? OnDataReceived { get; }

    /// <summary>
    /// Non-blocking method to process received data.
    /// </summary>
    protected Task Process();
}