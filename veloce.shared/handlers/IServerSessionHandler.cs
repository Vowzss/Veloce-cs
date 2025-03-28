using System.Net;
using veloce.shared.models;

namespace veloce.shared.handlers;

/// <summary>
/// Represents a base object for session handling on the server.
/// </summary>
public interface IServerSessionHandler : ISessionHandler<IServerSession>
{

}