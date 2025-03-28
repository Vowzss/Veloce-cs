using System.Net;
using veloce.shared.models;

namespace veloce.tests;

public sealed class DemoSession : AbstractSession
{
    public DemoSession(IPEndPoint endPoint) : base(endPoint)
    {
    }
}