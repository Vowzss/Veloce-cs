using System.Net;

namespace veloce.shared.events;

public abstract class AbstractEventArgs : IEventArgs
{
    public AbstractEventArgs(IPEndPoint sender)
    {
        Sender = sender;
    }

    public IPEndPoint Sender { get; }
}