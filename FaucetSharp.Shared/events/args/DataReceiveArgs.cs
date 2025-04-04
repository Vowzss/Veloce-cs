using System.Net;

namespace FaucetSharp.Shared.events;

public sealed class DataReceiveArgs : AbstractEventArgs
{
    public DataReceiveArgs(IPEndPoint sender, byte[] data) : base(sender)
    {
        Data = data;
    }

    public byte[] Data { get; init; }
}