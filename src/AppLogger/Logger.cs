namespace AppLogger;

public class Logger : ILogger
{
    public void Log(string text)
    {
        Console.WriteLine(text);
    }
}
