using System.Net;

namespace FaucetSharp.Models.Events.Args;

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