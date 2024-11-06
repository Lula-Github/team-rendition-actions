namespace AppLogger;

public enum LogLevel
{
    Debug,
    Info,
    Warning,
    Error,
    Critical
}

public class Logger : ILogger
{
    public void Log(string text)
    {
        Console.WriteLine(text);
    }

    public void Log(LogLevel level, string message)
    {
        Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff}] [{level}] {message}");
    }
}
