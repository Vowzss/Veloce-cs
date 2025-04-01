using System.Security.Cryptography;
using veloce.shared.models;
using veloce.shared.packets;
using veloce.shared.utils;

namespace veloce.shared.handlers;

public abstract class AbstractPacketSerializer : AbstractPacketHandler, IPacketSerializer
{
    public byte[] Write<TPacket>(TPacket packet, EncryptionContext? encryption) where TPacket : class, IPacket
    {
        // Serialize data using protobuf
        var rawData = PacketRegistry.Serialize(packet);

        // Case when packets cannot be secured
        // e.g. during handshake
        if (encryption == null)
            return rawData;
        
        // Encrypt data
        using var encryptor = encryption.GetEncryptor();
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        cs.Write(rawData, 0, rawData.Length);
        cs.FlushFinalBlock();
        
        return encryption.CopyIv(ms.ToArray());
    }
}

public sealed class DefaultPacketSerializer : AbstractPacketSerializer
{
    
}