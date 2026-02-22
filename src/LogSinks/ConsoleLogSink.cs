using System;

namespace WB.Logging;

/// <summary>
/// A log sink that writes log messages to the console.
/// </summary>
public sealed class ConsoleLogSink : ILogSink
{
    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Properties                                                           │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Gets or sets the format string for timestamps.
    /// </summary>
    public string TimestampFormat { get; init; } = "yyyy-MM-dd HH:mm:ss.fff";

    /// <summary>
    /// Gets or sets the color for timestamps.
    /// </summary>
    public ConsoleColor TimestampColor { get; init; } = ConsoleColor.DarkGray;

    /// <summary>
    /// Gets or sets the color for senders.
    /// </summary>
    public ConsoleColor SendersColor { get; init; } = ConsoleColor.Cyan;

    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                              │
    // └─────────────────────────────────────────────────────────────────────────────┘

    /// <inheritdoc/>
    public void Submit(LogMessage logMessage)
    {
        object? payload = logMessage.Message;

        Write(logMessage.Timestamp.ToString(TimestampFormat), TimestampColor);
        WriteSpace();

        if (payload is Exception)
        {
            WriteInBrackets("EXCE", ConsoleColor.White, ConsoleColor.Red);
        }
        else
        {
            WriteInBrackets(LogLevelToString(logMessage.LogLevel), LogLevelToColor(logMessage.LogLevel));
        }

        WriteSpace();
        WriteInBrackets(string.Join(" ", logMessage.Senders), SendersColor);

        if (payload is Exception exception)
        {
            WriteException(exception);
        }
        else
        {
            WritePayload(payload);
        }
    }

    // ┌─────────────────────────────────────────────────────────────────────────────┐
    // │ Private Methods                                                             │
    // └─────────────────────────────────────────────────────────────────────────────┘
    private static void WritePayload(object? payload)
    {
        int position = WriteSpace();
        string message = payload?.ToString() ?? string.Empty;
        string[] lines = Console.WindowWidth == 0 ? [message] : [.. message.WrapLines(Console.WindowWidth - position)];

        for (int i = 0; i < lines.Length - 1; i++)
        {
            Write(lines[i], ConsoleColor.White);
            WriteLine(position);
        }

        Write(lines[^1], ConsoleColor.White);
        WriteLine();
    }

    private static void WriteException(Exception exception)
    {
        int position = WriteSpace();

        if (exception.StackTrace is not null)
        {
            int width = Console.WindowWidth - position;

            string[] lines = [.. exception.ToString().WrapLines(width)];

            for (int i = 0; i < lines.Length - 1; i++)
            {
                Write(lines[i], ConsoleColor.Red);
                WriteLine(position);
            }

            Write(lines[^1], ConsoleColor.Red);
        }

        WriteLine();
    }

    private static int WriteSpace()
    {
        Console.Write(" ");

        return Console.CursorLeft;
    }

    private static void WriteLine()
        => Console.WriteLine();

    private static void WriteLine(int position)
    {
        Console.WriteLine();

        Console.CursorLeft = position;
    }

    private static string LogLevelToString(LogLevel? logLevel)
        => logLevel switch
        {
            LogLevel.Info => "INFO",
            LogLevel.Warning => "WARN",
            LogLevel.Error => "ERRO",
            null => "NONE",
            _ => "UNKN"
        };

    private static ConsoleColor LogLevelToColor(LogLevel? logLevel)
        => logLevel switch
        {
            LogLevel.Info => ConsoleColor.Green,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            null => ConsoleColor.Gray,
            _ => ConsoleColor.White
        };

    private static int Write(string message, ConsoleColor color)
    {
        ConsoleColor previousColor = Console.ForegroundColor;

        Console.ForegroundColor = color;
        Console.Write(message);

        Console.ForegroundColor = previousColor;

        return Console.CursorLeft;
    }

    private static int WriteInBrackets(string message, ConsoleColor color)
    {
        ConsoleColor previousColor = Console.ForegroundColor;

        Console.Write("[");

        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ForegroundColor = previousColor;

        Console.Write("]");

        return Console.CursorLeft;
    }

    private static void WriteInBrackets(string message, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        ConsoleColor previousForegroundColor = Console.ForegroundColor;
        ConsoleColor previousBackgroundColor = Console.BackgroundColor;

        Console.Write("[");

        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
        Console.Write(message);
        Console.ForegroundColor = previousForegroundColor;
        Console.BackgroundColor = previousBackgroundColor;

        Console.Write("]");
    }
}