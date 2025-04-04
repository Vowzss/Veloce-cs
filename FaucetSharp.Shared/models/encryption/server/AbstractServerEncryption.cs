using System.Security.Cryptography;

namespace FaucetSharp.Shared.models;

public abstract class AbstractServerEncryption : AbstractEncryption, IServerEncryption
{
    public void LoadAesKey(RSA rsa, byte[] aesKey)
    {
        AesKey = rsa.Decrypt(aesKey, RSAEncryptionPadding.OaepSHA256);
    }
}