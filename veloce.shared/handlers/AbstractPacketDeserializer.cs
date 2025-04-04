using System.Security.Cryptography;
using veloce.shared.exceptions;
using veloce.shared.models;
using veloce.shared.packets;
using Exception = System.Exception;

namespace veloce.shared.handlers;

public abstract class AbstractPacketDeserializer : IPacketDeserializer
{
    public IPacket Read(byte[] data, IEncryptionContext encryption)
    {
        if (!encryption.IsSecure())
        {
            // Edge case: handshake packet is never encrypted
            var packet = PacketRegistry.Deserialize(data);
            if (packet is IHandshakePacket)
                return packet;
            
            // Cancel reading process when encryption not available
            throw new EncryptionNotValid();
        }

        // Load aes iv from encrypted data for deserialization
        encryption.LoadAesIv(ref data);

        // Decrypt data
        using var decryptor = encryption.GetDecryptor();
        using var ms = new MemoryStream(data);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var reader = new BinaryReader(cs);

        // Read the decrypted data
        var rawData = reader.ReadBytes(data.Length).ToArray();

        // Return deserialized data using protobuf
        return PacketRegistry.Deserialize(rawData);
    }
}