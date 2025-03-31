﻿using System.Net;
using veloce.shared.models;

namespace veloce.shared.handlers;

/// <summary>
/// Represents a base object for session handling.
/// </summary>
public interface ISessionHandler<TSession>
    where TSession : class, ISession
{
    public IDictionary<string, TSession> Sessions { get; }
    
    public TSession? Get(string endpoint);
}