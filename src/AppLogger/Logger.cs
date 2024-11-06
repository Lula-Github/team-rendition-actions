namespace AppLogger;

public enum LogLevel
{
    Trace,
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
        var log = CreateLogEntry(level, message);
        WriteToConsole(level, log);
    }

    private string CreateLogEntry(LogLevel level, string message)
    {
        return $"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff}] [{level}] {message}";
    }

    private void WriteToConsole(LogLevel level, string message)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = GetColorForLogLevel(level);
        Console.WriteLine(message);
        Console.ForegroundColor = originalColor;
    }

    private ConsoleColor GetColorForLogLevel(LogLevel level)
    {
        return level switch
        {
            LogLevel.Debug => ConsoleColor.Blue,
            LogLevel.Info => ConsoleColor.Green,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Critical => ConsoleColor.DarkRed,
            _ => ConsoleColor.White
        };
    }
}
