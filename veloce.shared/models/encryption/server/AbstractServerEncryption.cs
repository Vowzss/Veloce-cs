using System.Security.Cryptography;

namespace veloce.shared.models;

public abstract class AbstractServerEncryption : AbstractEncryption, IServerEncryption
{
    public void LoadAesKey(RSA rsa, byte[] aesKey)
    {
        AesKey = rsa.Decrypt(aesKey, RSAEncryptionPadding.OaepSHA256);
    }
}