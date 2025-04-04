namespace FaucetSharp.Shared.Extensions;

public static class DateTimeExtensions
{
    public static long NowMs => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}