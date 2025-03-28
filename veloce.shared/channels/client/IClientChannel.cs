using veloce.shared.interceptors.client;
using veloce.shared.models;
using veloce.shared.packets;

namespace veloce.shared.channels.client;

public interface IClientChannel : IChannel<IClientPacketInterceptor>
{
    /// <summary>
    /// Represents the object for encryption values.
    /// </summary>
    protected EncryptionContext? Encryption { get; }
    
    protected void Send(IPacket packet);
}