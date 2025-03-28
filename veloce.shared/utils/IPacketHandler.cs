using System.Security.Cryptography;
using veloce.shared.packets;

namespace veloce.shared.utils;

public interface IPacketHandler
{
    protected RSA Rsa { get; }
    protected Aes Aes { get; }

    protected byte[] AesKey { get; }
    protected byte[] AesIV { get; }
    
    public string GetPublicKey();
    public string GetPrivateKey();
}

public interface IPacketSerializer : IPacketHandler
{
    /// <summary>
    /// Method to transform a packet into a binary format.
    /// </summary>
    public byte[] Write<TPacket>(TPacket packet) where TPacket : class, IPacket;
}

public interface IPacketDeserializer : IPacketHandler
{
    /// <summary>
    /// Method to transform a binary format into a packet.
    /// </summary>
    public IPacket Read(byte[] data);
}