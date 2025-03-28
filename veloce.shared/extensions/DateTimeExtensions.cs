namespace veloce.shared.extensions;

public static class DateTimeExtensions
{
    public static long Now => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}