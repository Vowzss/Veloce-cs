using System.Security.Cryptography;

namespace veloce.shared.models;

public abstract class AbstractClientEncryption : AbstractEncryption, IClientEncryption
{
    public byte[] GenerateAesKey(byte[] rsaKey)
    {
        // Create rsa algorithm instance and load server's public key
        using var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(rsaKey, out _);

        Aes.GenerateKey();

        // Make sure to keep the raw aes key for later data transformation
        AesKey = Aes.Key;

        // Encrypt aes key for symmetric encryption
        return rsa.Encrypt(AesKey, RSAEncryptionPadding.OaepSHA256);
    }
}