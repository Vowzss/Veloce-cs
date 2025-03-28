using System.Security.Cryptography;
using ProtoBuf.Meta;
using veloce.shared.packets;

namespace veloce.shared.utils;

/// <summary>
/// Represents a unique object for packet serialization and deserialization.
/// </summary>
/// <remarks>This object is a wrapper around protobuf to dynamically manage packet resolution.</remarks>
public abstract class PacketHandler : IPacketHandler
{
    private static RuntimeTypeModel Registry { get; } = RuntimeTypeModel.Create();
    private static readonly IDictionary<Type, int> Indexes = new Dictionary<Type, int>();
    
    public RSA Rsa { get; } = RSA.Create();
    public Aes Aes { get; } = Aes.Create();
    
    public byte[] AesKey { get; protected set;}
    public byte[] AesIV { get; protected set; }
    
    /// <summary>
    /// Method to register packet types.
    /// </summary>
    public static void RegisterPacketType<TPacketBase, TPacket>()
        where TPacketBase : class
        where TPacket : TPacketBase
    {
        if (!Indexes.ContainsKey(typeof(TPacketBase)))
            Indexes.Add(typeof(TPacketBase), 100);
        
        var index = Indexes[typeof(TPacketBase)];
        Registry.Add<TPacketBase>()
            .AddSubType(index, typeof(TPacket));
        Indexes[typeof(TPacketBase)] = index + 1;
    }

    public string GetPublicKey() => Rsa.ExportRSAPublicKeyPem();
    public string GetPrivateKey() => Rsa.ExportRSAPrivateKeyPem();
    
    public byte[] Write<TPacket>(TPacket packet) where TPacket : class, IPacket
    {
        // Serialize data using protobuf
        using var stream = new MemoryStream();
        Registry.Serialize(stream, packet);
        var rawData = stream.ToArray();
        
        // Encrypt data
        using var encryptor = Aes.CreateEncryptor(AesKey, AesIV);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        cs.Write(rawData, 0, rawData.Length);
        cs.FlushFinalBlock();
        
        // Return encrypted data
        return ms.ToArray();
    }
    
    public IPacket Read(byte[] data)
    {
        // Decrypt data
        using var decryptor = Aes.CreateDecryptor(AesKey, AesIV);
        using var ms = new MemoryStream(data);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var reader = new BinaryReader(cs);
        
        // Read the decrypted data
        var rawData = new ReadOnlyMemory<byte>(reader.ReadBytes(data.Length).ToArray());
        
        // Return deserialized data using protobuf
        return Registry.Deserialize<AbstractPacket>(rawData);
    }
}