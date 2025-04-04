namespace FaucetSharp.Shared.exceptions;

public sealed class EncryptionNotValid() : Exception("Encryption context must be valid!")
{
}