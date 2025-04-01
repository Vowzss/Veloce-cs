using System.Security.Cryptography;

namespace veloce.shared.models;

/// <summary>
/// Represents an object containing end-to-end encryption values.
/// </summary>
public sealed class EncryptionContext
{
    /// <summary>
    /// AES specification (FIPS PUB 197)
    /// <para>https://csrc.nist.gov/pubs/fips/197/final</para>
    /// </summary>
    private const int AesIvLength = 16;
    
    /// <summary>
    /// Represents the aes algorithm instance.
    /// </summary>
    private Aes Aes { get; }

    /// <summary>
    /// Represents the symmetric encryption key.
    /// </summary>
    private byte[]? AesKey { get; set; }
    
    /// <summary>
    /// Represents the randomness value for secure encryption/decryption.
    /// </summary>
    /// <remarks>
    /// <para>This value is generated for each serialization to ensure data integrity.</para>
    /// <para>During deserialization, it must always match the value used to serialize the data.</para>
    /// </remarks>
    private byte[]? AesIv { get; set; }

    public EncryptionContext()
    {
        Aes = Aes.Create();
        Aes.Mode = CipherMode.CBC;
        Aes.Padding = PaddingMode.PKCS7;
    }

    /// <summary>
    /// Returns whether the context was correctly initialized.
    /// </summary>
    public bool IsValid() => AesKey != null;

    /// <summary>
    /// Method to create an object for encryption.
    /// </summary>
    /// <remarks><c>iv</c> rolling is automatically handled by this method for security purposes.</remarks>
    public ICryptoTransform GetEncryptor()
    {
        Aes.GenerateIV();
        AesIv = Aes.IV;
        return Aes.CreateEncryptor(AesKey!, AesIv);
    }
    
    /// <summary>
    /// Method to create an object for decryption. 
    /// </summary>
    /// <remarks>The <c>iv</c> parameter must match the value that serialized the data in the first place.</remarks>
    public ICryptoTransform GetDecryptor()
    {
        return Aes.CreateDecryptor(AesKey!, AesIv);
    }
    
    /// <summary>
    /// Method to read the iv at the beginning of the data for deserialization.
    /// </summary>
    /// <remarks>This method must be called upon receiving data.</remarks>
    public void LoadIv(byte[] data)
    {
        var iv = new byte[AesIvLength];
        Array.Copy(data, 0, iv, 0, AesIvLength);
        
        var encryptedData = new byte[data.Length - AesIvLength];
        Array.Copy(data, AesIvLength, encryptedData, 0, encryptedData.Length);

        AesIv = iv;
    }
    
    /// <summary>
    /// Method to copy the iv at the beginning of the data for later deserialization.
    /// </summary>
    /// <remarks>This method must be called upon sending data.</remarks>
    public byte[] CopyIv(byte[] data)
    {
        var result = new byte[AesIvLength + data.Length];
        
        Array.Copy(AesIv!, 0, result, 0, AesIvLength);
        Array.Copy(data, 0, result, AesIvLength, data.Length);
        
        return result;
    }

    public void LoadAesKey(RSA rsa, byte[] key)
    {
        AesKey = rsa.Decrypt(key, RSAEncryptionPadding.OaepSHA256);
    }
}