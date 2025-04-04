namespace veloce.shared.models;

/// <summary>
///     Represents an object containing end-to-end encryption values specifically for the client.
/// </summary>
public interface IClientEncryption : IEncryptionContext
{
    /// <summary>
    ///     Method to generate an encrypted aes key using server's public rsa key.
    /// </summary>
    public byte[] GenerateAesKey(byte[] rsaKey);
}