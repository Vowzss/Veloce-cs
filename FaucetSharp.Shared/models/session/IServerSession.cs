using FaucetSharp.Shared.enums;

namespace FaucetSharp.Shared.models;

/// <summary>
///     Represents an object to manage a connection specifically on the server.
/// </summary>
public interface IServerSession : ISession<IServerEncryption>
{
    /// <summary>
    ///     Represents the client status.
    /// </summary>
    public ClientStatus Status { get; }

    /// <summary>
    ///     Represents the timestamp of the last packet received from the client.
    /// </summary>
    public long? LastSeenAt { get; }

    /// <summary>
    ///     Represents the timestamp when the client was disconnected.
    /// </summary>
    public long? DisconnectAt { get; }

    /// <summary>
    ///     Utility method to indicate connection health.
    /// </summary>
    public bool IsHealthy();
}