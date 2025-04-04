using System.Security.Cryptography;

namespace FaucetSharp.Models.Objects.Encryption;

/// <summary>
///     Represents an object containing end-to-end encryption values.
/// </summary>
public interface IEncryptionContext
{
    /// <summary>
    ///     AES specification (FIPS PUB 197)
    ///     <para>https://csrc.nist.gov/pubs/fips/197/final</para>
    /// </summary>
    protected const int AesIvLength = 16;

    /// <summary>
    ///     Represents the aes algorithm instance.
    /// </summary>
    protected Aes Aes { get; }

    /// <summary>
    ///     Represents the symmetric encryption key.
    /// </summary>
    protected byte[]? AesKey { get; set; }

    /// <summary>
    ///     Represents the randomness value for secure encryption/decryption.
    /// </summary>
    /// <remarks>
    ///     <para>This value is generated for each serialization to ensure data integrity.</para>
    ///     <para>During deserialization, it must always match the value used to serialize the data.</para>
    /// </remarks>
    protected byte[]? AesIv { get; set; }
    
    /// <summary>
    ///     Returns whether the context was correctly setup or not.
    /// </summary>
    public bool IsSecure();

    /// <summary>
    ///     Method to create an object for encryption.
    /// </summary>
    /// <remarks><c>iv</c> rolling is automatically handled by this method for security purposes.</remarks>
    public ICryptoTransform GetEncryptor()
    {
        Aes.GenerateIV();
        RandomNumberGenerator.Fill(Aes.IV); // Extra security

        AesIv = Aes.IV;
        return Aes.CreateEncryptor(AesKey!, AesIv);
    }

    /// <summary>
    ///     Method to create an object for decryption.
    /// </summary>
    /// <remarks>The <c>iv</c> parameter must match the value that serialized the data in the first place.</remarks>
    public ICryptoTransform GetDecryptor()
    {
        return Aes.CreateDecryptor(AesKey!, AesIv);
    }

    /// <summary>
    ///     Method to read the aes iv at the beginning of the data for deserialization.
    /// </summary>
    /// <remarks>This method must be called upon receiving data.</remarks>
    public void LoadAesIv(ref byte[] data)
    {
        // Extract aes iv from the data
        AesIv = data.Take(AesIvLength).ToArray();

        // Erase the iv from the data to avoid corrupted deserialization
        data = data.Skip(AesIvLength).ToArray();
    }

    /// <summary>
    ///     Method to copy the aes iv at the beginning of the data for later deserialization.
    /// </summary>
    /// <remarks>This method must be called upon sending data.</remarks>
    public void CopyAesIv(ref byte[] data)
    {
        var result = new byte[AesIvLength + data.Length];
        Array.Copy(AesIv!, 0, result, 0, AesIvLength);
        Array.Copy(data, 0, result, AesIvLength, data.Length);
        data = result;
    }
}