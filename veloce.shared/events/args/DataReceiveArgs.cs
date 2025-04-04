using System.Net;

namespace veloce.shared.events;

public sealed class DataReceiveArgs : AbstractEventArgs
{
    public DataReceiveArgs(IPEndPoint sender, byte[] data) : base(sender)
    {
        Data = data;
    }

    public byte[] Data { get; init; }
}