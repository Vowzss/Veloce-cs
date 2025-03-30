using System.Net;
using veloce.shared.packets;

namespace veloce.shared.events;

/// <summary>
/// Represents the base object for packet event arguments.
/// </summary>
public interface IEventPacketArgs : IEventArgs
{
    /// <summary>
    /// Represents the packet sent by the client.
    /// </summary>
    public IPacket Packet { get; }
}