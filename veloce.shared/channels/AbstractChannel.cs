using System.Net;
using System.Net.Sockets;
using Serilog;
using veloce.shared.events;

namespace veloce.shared.channels;

public abstract class AbstractChannel : IChannel
{
    public ILogger Logger { get; }
    
    public bool HasAuthority { get; }
    public IPEndPoint EndPoint { get; }
    
    public CancellationTokenSource Signal { get; }
    public DataReceiveEvent? OnDataReceived { get; set; }

    protected UdpClient Transport { get; }
    
    protected AbstractChannel(IPEndPoint endPoint, bool hasAuthority = false)
    {
        Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        
        HasAuthority = hasAuthority;
        EndPoint = endPoint;
        Signal = new CancellationTokenSource();
        
        try
        {
            Transport = hasAuthority ? new UdpClient(EndPoint) : new UdpClient();
        }
        catch (SocketException e)
        {
            Logger.Fatal($"Cannot connect to {endPoint}.", e);
            throw new Exception($"Cannot connect to {endPoint}.", e);
        }
    }

    public abstract Task Process();
}