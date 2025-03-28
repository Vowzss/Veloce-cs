﻿using System.Net;
using veloce.shared.enums;

namespace veloce.shared.models;

/// <summary>
/// Represents an object to manage a client connection.
/// </summary>
public interface ISession
{
    /// <summary>
    /// Represents the client remote endpoint.
    /// </summary>
    public IPEndPoint EndPoint { get; }
    
    /// <summary>
    /// Represents the client encryption values.
    /// </summary>
    public EncryptionContext Encryption { get; }
    
    /// <summary>
    /// Represents the client session id.
    /// </summary>
    /// <remarks>
    /// <para>This value must be unique.</para>
    /// <para>Unless specified, <see cref="EndPoint"/> acts as such.</para>
    /// </remarks>
    public string SessionId { get; }

    /// <summary>
    /// Represents the client status.
    /// </summary>
    public ClientStatus Status { get; }
    
    /// <summary>
    /// Represents the timestamp of the last packet received from the client.
    /// </summary>
    public long? LastSeenAt { get; }
    
    /// <summary>
    /// Represents the timestamp when the client was disconnected.
    /// </summary>
    public long? DisconnectAt { get; }
}