using System.Net;

namespace FaucetSharp.Models.Events.Args;

public abstract class AbstractEventArgs : IEventArgs
{
    public AbstractEventArgs(IPEndPoint sender)
    {
        Sender = sender;
    }

    public IPEndPoint Sender { get; }
}