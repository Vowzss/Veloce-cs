using System.Net;

namespace FaucetSharp.Shared.events;

/// <summary>
///     Represents the base object for event arguments.
/// </summary>
public interface IEventArgs
{
    /// <summary>
    ///     Represents the source of the event.
    /// </summary>
    public IPEndPoint Sender { get; }
}