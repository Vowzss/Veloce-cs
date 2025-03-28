using veloce.shared.packets;
using veloce.shared.utils;

namespace veloce.shared.interceptors;

/// <summary>
/// Represents an object to handle packets received from transport.
/// </summary>
public interface IPacketInterceptor
{
    /// <summary>
    /// Represents the handler for packet deserialization
    /// </summary>
    protected IPacketDeserializer Deserilizer { get; }
    
    /// <summary>
    /// Non-blocking method to resolve packet from data.
    /// </summary>
    public void Accept(byte[] data);
    
    /// <summary>
    /// Non-blocking method to process custom packets.
    /// </summary>
    public void Handle(IPacket packet);
}