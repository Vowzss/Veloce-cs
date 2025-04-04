using FaucetSharp.Models.Packets;

namespace FaucetSharp.Models.Packets;

/// <summary>
///     Represents a data object to acknowledge network communication.
/// </summary>
public interface IAckPacket : IPacket
{
    /// <summary>
    ///     Indicates whether the server has successfully processed the packet.
    /// </summary>
    public bool IsSuccessful { get; }
}