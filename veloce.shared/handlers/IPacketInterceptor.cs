using veloce.shared.packets;

namespace veloce.shared.handlers;

/// <summary>
/// Represents an object to handle packets received from transport.
/// </summary>
public interface IPacketInterceptor
{
    /// <summary>
    /// Non-blocking method to resolve packet from data.
    /// </summary>
    public void Accept(byte[] data);
    
    /// <summary>
    /// Non-blocking method to process custom packets.
    /// </summary>
    public void Handle(IPacket packet);
}