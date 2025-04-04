namespace FaucetSharp.Shared.Exceptions;

public sealed class EncryptionNotValid() : Exception("Encryption context must be valid!")
{
}