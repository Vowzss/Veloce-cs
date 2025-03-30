using System.Net;
using veloce.shared.packets;

namespace veloce.shared.events;

public abstract class AbstractEventArgs : IEventArgs
{
    public IPEndPoint Sender { get; }

    public AbstractEventArgs(IPEndPoint sender)
    {
        Sender = sender;
    }
}