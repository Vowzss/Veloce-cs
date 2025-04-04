namespace FaucetSharp.Models.Enums;

public enum HandshakeStep
{
    Unknown = 0,
    Establishing,
    PublicKey,
    AesKey,
    Established
}