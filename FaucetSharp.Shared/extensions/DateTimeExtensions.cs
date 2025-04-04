namespace FaucetSharp.Shared.extensions;

public static class DateTimeExtensions
{
    public static long NowMs => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}