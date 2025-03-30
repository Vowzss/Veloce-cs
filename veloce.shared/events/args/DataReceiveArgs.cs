using System.Net;

namespace veloce.shared.events;

public sealed class DataReceiveArgs : AbstractEventArgs
{
    public byte[] Data { get; init; }
    
    public DataReceiveArgs(IPEndPoint sender, byte[] data) : base(sender)
    {
        Data = data;
    }
}
