using FaucetSharp.Shared.packets;

namespace FaucetSharp.Shared.events;

/// <summary>
///     Represents the base object for packet event arguments.
/// </summary>
public interface IEventPacketArgs : IEventArgs
{
    /// <summary>
    ///     Represents the packet sent by the client.
    /// </summary>
    public IPacket Packet { get; }
}