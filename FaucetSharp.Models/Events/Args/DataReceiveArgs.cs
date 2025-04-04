using System.Net;
using FaucetSharp.Models.Events.Args;

namespace FaucetSharp.Models.Events.Args;

public sealed class DataReceiveArgs : AbstractEventArgs
{
    public DataReceiveArgs(IPEndPoint sender, byte[] data) : base(sender)
    {
        Data = data;
    }

    public byte[] Data { get; init; }
}