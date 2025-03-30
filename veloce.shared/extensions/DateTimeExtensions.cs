namespace veloce.shared.extensions;

public static class DateTimeExtensions
{
    public static long NowMs => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}