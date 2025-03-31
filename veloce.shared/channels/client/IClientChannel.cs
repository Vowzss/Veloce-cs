using veloce.shared.interceptors.client;
using veloce.shared.models;
using veloce.shared.packets;

namespace veloce.shared.channels.client;

/// <summary>
/// Represents a channel object designed for client communication.
/// </summary>
public interface IClientChannel : IChannel<IClientPacketInterceptor>
{
    /// <summary>
    /// Represents the object for encryption values.
    /// </summary>
    protected EncryptionContext? Encryption { get; }
    
    /// <summary>
    /// Non-block method to send a packet to a server.
    /// </summary>
    protected Task Send(IPacket packet);
}