namespace FaucetSharp.Models.Enums;

public enum ClientStatus
{
    Unknown = 0,
    Connecting,
    Disconnected,
    Reconnecting,
    Connected
}