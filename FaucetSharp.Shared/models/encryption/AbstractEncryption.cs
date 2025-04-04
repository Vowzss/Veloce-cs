using System.Security.Cryptography;

namespace FaucetSharp.Shared.models;

public abstract class AbstractEncryption : IEncryptionContext
{
    public Aes Aes { get; }

    public byte[]? AesKey { get; set; }
    public byte[]? AesIv { get; set; }

    public bool IsSecure() => AesKey != null;

    protected AbstractEncryption()
    {
        Aes = Aes.Create();
        Aes.KeySize = 256;
        Aes.Mode = CipherMode.CBC;
        Aes.Padding = PaddingMode.PKCS7;
    }
}