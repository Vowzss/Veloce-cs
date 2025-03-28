using System.Security.Cryptography;
using veloce.shared.models;
using veloce.shared.packets;

namespace veloce.shared.utils;

/// <summary>
/// Represents an object for secure packet deserialization.
/// </summary>
public abstract class AbstractPacketDeserializer : AbstractPacketSerializer, IPacketDeserializer
{
    public IPacket Read(byte[] data, EncryptionContext encryption)
    {
        encryption.LoadIv(data);
        
        // Decrypt data
        using var decryptor = encryption.GetDecryptor(encryption.AesIv);
        using var ms = new MemoryStream(data);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var reader = new BinaryReader(cs);
        
        // Read the decrypted data
        var rawData = new ReadOnlyMemory<byte>(reader.ReadBytes(data.Length).ToArray());
        
        // Return deserialized data using protobuf
        return Registry.Deserialize<AbstractGamePacket>(rawData);
    }
}