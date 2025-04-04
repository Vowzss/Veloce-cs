using FaucetSharp.Models.Packets;

namespace FaucetSharp.Models.Packets;

/// <summary>
///     Represents a data object for game communication.
/// </summary>
public interface IGamePacket : IPacket
{
    /// <summary>
    ///     Represents the client who sent the packet.
    /// </summary>
    public string PlayerId { get; }
}