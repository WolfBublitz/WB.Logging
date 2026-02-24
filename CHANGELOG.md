# WB.Logging

## 1.0.0 - Initial Release

### ✨ Features

- `Logger` class that implements the `ILogger` interface, providing methods for logging messages with different severity levels (Info, Warning, Error).
- `ILogSink` interface that defines the contract for log sinks, allowing for extensibility and customization of log output.
- `ConsoleLogSink` class that implements the `ILogSink` interface, enabling logging to the console using `System.Console`.
- Extension method `AttachConsole()` for the `ILogger` interface, allowing for easy attachment of a console log sink to any logger instance.
