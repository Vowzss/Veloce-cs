using System.Security.Cryptography;

namespace FaucetSharp.Models.Objects.Encryption.Server;

public abstract class AbstractServerEncryption : AbstractEncryption, IServerEncryption
{
    public void LoadAesKey(RSA rsa, byte[] aesKey)
    {
        AesKey = rsa.Decrypt(aesKey, RSAEncryptionPadding.OaepSHA256);
    }
}