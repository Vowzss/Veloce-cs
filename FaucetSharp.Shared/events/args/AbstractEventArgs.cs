using System.Net;

namespace FaucetSharp.Shared.events;

public abstract class AbstractEventArgs : IEventArgs
{
    public AbstractEventArgs(IPEndPoint sender)
    {
        Sender = sender;
    }

    public IPEndPoint Sender { get; }
}