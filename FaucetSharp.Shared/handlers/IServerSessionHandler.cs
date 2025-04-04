using System.Net;
using FaucetSharp.Shared.models;

namespace FaucetSharp.Shared.handlers;

/// <summary>
///     Represents a base object for session handling on the server.
/// </summary>
public interface IServerSessionHandler
{
    /// <summary>
    ///     Contains all sessions mapped by the id for searching purposes.
    /// </summary>
    protected IDictionary<string, IServerSession> Sessions { get; }

    /// <summary>
    ///     Utility method to create a custom session id.
    /// </summary>
    public string ComputeId(IPEndPoint endpoint);

    /// <summary>
    ///     Method to register a session based on the client's endpoint.
    /// </summary>
    public IServerSession? Register(IPEndPoint endpoint);

    /// <summary>
    ///     Base method to find a session based on an id.
    /// </summary>
    public IServerSession? FindById(string id);

    /// <summary>
    ///     Method to find a session based on an endpoint.
    /// </summary>
    /// <remarks>
    ///     <para>This method evaluates an id based on <see cref="ComputeId(IPEndPoint)" />.</para>
    ///     <para>It subsequently calls for base method <see cref="FindById(string)" />.</para>
    /// </remarks>
    public IServerSession? FindByEndpoint(IPEndPoint endpoint);

    /// <summary>
    ///     Method to retrieve all connected sessions.
    /// </summary>
    public IList<IServerSession> GetAll();
}