using System.Security.Cryptography;
using FaucetSharp.Shared.exceptions;
using FaucetSharp.Shared.models;
using FaucetSharp.Shared.packets;
using Exception = System.Exception;

namespace FaucetSharp.Shared.handlers;

public abstract class AbstractPacketDeserializer : IPacketDeserializer
{
    public IPacket Read(byte[] data, IEncryptionContext encryption)
    {
        try
        {
            // Edge case: handshake packet is never encrypted
            var packet = PacketRegistry.Deserialize(data);
            if (packet is IHandshakePacket)
                return packet;
        }
        catch
        {
            // ignored
        }
        
        // Cancel reading process when encryption not available
        if (!encryption.IsSecure())
            throw new EncryptionNotValid();

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