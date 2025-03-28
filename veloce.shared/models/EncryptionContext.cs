using System.Security.Cryptography;

namespace veloce.shared.models;

/// <summary>
/// Represents an object containing end-to-end encryption values.
/// </summary>
public struct EncryptionContext(byte[] rsaKey, byte[] aesKey)
{
    /// <summary>
    /// AES specification (FIPS PUB 197)
    /// <para>https://csrc.nist.gov/pubs/fips/197/final</para>
    /// </summary>
    private const int AesIvLength = 16;
    
    /// <summary>
    /// Represents the aes algorithm instance.
    /// </summary>
    private Aes Aes { get;} = Aes.Create();
    
    /// <summary>
    /// Represents the rsa public key.
    /// </summary>
    private byte[] RsaKey { get;} = rsaKey;

    /// <summary>
    /// Represents the symmetric encryption key.
    /// </summary>
    private byte[] AesKey { get; } = aesKey;
    
    /// <summary>
    /// Represents the randomness value for encryption.
    /// </summary>
    public byte[] AesIv { get; private set; }

    /// <summary>
    /// Method to create an object for encryption.
    /// </summary>
    /// <remarks><c>iv</c> rolling is automatically handled by this method for security purposes.</remarks>
    public ICryptoTransform GetEncryptor()
    {
        Aes.GenerateIV();
        //AesIv = Aes.IV;
        return Aes.CreateEncryptor(AesKey, Aes.IV);
    }
    
    /// <summary>
    /// Method to create an object for decryption. 
    /// </summary>
    /// <remarks>The <c>iv</c> parameter must match the value that serialized the data in the first place.</remarks>
    public ICryptoTransform GetDecryptor(byte[] iv) => Aes.CreateDecryptor(AesKey, iv);
    
    /// <summary>
    /// Method to read the iv at the beginning of the data for deserialization.
    /// </summary>
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
    public byte[] CopyIv(byte[] data)
    {
        var result = new byte[AesIvLength + data.Length];
        
        Array.Copy(AesIv, 0, result, 0, AesIvLength);
        Array.Copy(data, 0, result, AesIvLength, data.Length);
        
        return result;
    }
}