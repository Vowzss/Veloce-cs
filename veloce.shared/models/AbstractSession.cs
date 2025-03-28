﻿using System.Net;
using veloce.shared.enums;
using veloce.shared.extensions;

namespace veloce.shared.models;

public abstract class AbstractSession : ISession
{
    public IPEndPoint EndPoint { get; protected init; }
    public string SessionId { get; protected init; }
    
    public ClientStatus Status { get; protected set; }
    public long? LastSeenAt { get; protected set; }
    public long? DisconnectAt { get; protected set; }

    protected AbstractSession(IPEndPoint endPoint)
    {
        EndPoint = endPoint;
        SessionId = HashCode.Combine(EndPoint).ToString();
        
        Status = ClientStatus.Unknown;
    }
}

public sealed class DefaultSession : AbstractSession
{
    public DefaultSession(IPEndPoint endPoint) : base(endPoint)
    {
    }
}