using System.Security.Cryptography;
using veloce.shared.models;
using veloce.shared.packets;
using veloce.shared.utils;

namespace veloce.shared.handlers;

public abstract class AbstractPacketDeserializer : AbstractPacketSerializer, IPacketDeserializer
{
    public IPacket Read(byte[] data, EncryptionContext? encryption)
    {
        // Case when packets cannot be secured
        // e.g. during handshake
        if (encryption is null)
            return PacketRegistry.Deserialize(data);
        
        // Load aes iv from serialized data for deserialization
        encryption.Value.LoadIv(data);
        
        // Decrypt data
        using var decryptor = encryption.Value.GetDecryptor(encryption.Value.AesIv);
        using var ms = new MemoryStream(data);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var reader = new BinaryReader(cs);
        
        // Read the decrypted data
        var rawData = reader.ReadBytes(data.Length).ToArray();
        
        // Return deserialized data using protobuf
        return PacketRegistry.Deserialize(rawData);
    }
}

public sealed class DefaultPacketDeserializer : AbstractPacketDeserializer
{
    
}