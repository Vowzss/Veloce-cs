﻿using System.Security.Cryptography;
using FaucetSharp.Core.Handlers;
using FaucetSharp.Core.Utils;
using FaucetSharp.Models.Objects.Encryption;
using FaucetSharp.Models.Packets;
using FaucetSharp.Models.Packets.Handshake;
using FaucetSharp.Shared.Exceptions;

namespace FaucetSharp.Core.Handlers;

public abstract class AbstractPacketSerializer : IPacketSerializer
{
    public byte[] Write<TPacket>(TPacket packet, IEncryptionContext encryption) where TPacket : class, IPacket
    {
        // Serialize data using protobuf
        var rawData = PacketRegistry.Serialize(packet);

        // Edge case: handshake packet is never encrypted
        if (packet is IHandshakePacket)
            return rawData;

        // Cancel writing process when encryption not available
        if (!encryption.IsSecure())
            throw new EncryptionNotValid();

        // Encrypt data
        using var encryptor = encryption.GetEncryptor();
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        {
            cs.Write(rawData, 0, rawData.Length);
            cs.FlushFinalBlock();
        }

        var encryptedData = ms.ToArray();
        encryption.CopyAesIv(ref encryptedData);
        return encryptedData;
    }
}