using System.Security.Cryptography;

namespace FaucetSharp.Models.Objects.Encryption.Server;

/// <summary>
///     Represents an object containing end-to-end encryption values specifically for the server.
/// </summary>
/// <remarks>This object is tied to the client's session, therefor containing encryption values related to it.</remarks>
public interface IServerEncryption : IEncryptionContext
{
    /// <summary>
    ///     Method to load the client's encrypted aes key.
    /// </summary>
    /// <remarks>This method will decrypt the aes key using the private rsa key.</remarks>
    public void LoadAesKey(RSA rsa, byte[] aesKey);
}