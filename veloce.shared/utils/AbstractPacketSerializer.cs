using System.Security.Cryptography;
using veloce.shared.models;
using veloce.shared.packets;

namespace veloce.shared.utils;

/// <summary>
/// Represents an object for secure packet serialization.
/// </summary>
public abstract class AbstractPacketSerializer : AbstractPacketHandler, IPacketSerializer
{
    public byte[] Write<TPacket>(TPacket packet, EncryptionContext encryption) where TPacket : class, IPacket
    {
        // Serialize data using protobuf
        using var stream = new MemoryStream();
        Registry.Serialize(stream, packet);
        var rawData = stream.ToArray();
        
        // Encrypt data
        using var encryptor = encryption.GetEncryptor();
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        cs.Write(rawData, 0, rawData.Length);
        cs.FlushFinalBlock();
        
        return encryption.CopyIv(ms.ToArray());
    }
}