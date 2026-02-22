# WB.Logging

An easy-to-use non-blocking logging library for .NET applications, designed to provide a simple and efficient way to log messages with various levels of severity.

## ✨ Features

- **Non-blocking**: Logs are processed asynchronously, ensuring that your application remains responsive.
- **Multiple Sinks**: Supports multiple output targets (sinks) such as console, file, and more.
- **Configurable**: Easily configure log levels, formats, and sinks to suit your needs.
- **Extensible**: Create custom sinks and loggers to fit your specific requirements.
- **Log anything**: Log messages of any type, including complex objects, with automatic serialization.

## 🚀 Example

```csharp
using WB.Logging;

// Step 1:  Create a logger with name "MyApp"
await using ILogger logger = new Logger("MyApp");

// Step 2:  Attach a console sink to the logger
logger.AttachConsole();

// Step 3:  Log messages with different severity levels
logger.LogInfo("This is an informational message.");
logger.LogWarning("This is a warning message.");
logger.LogError("This is an error message.");
```

**Notes:**

- Check the `await using` statement in the example, which ensures that the logger is properly disposed of after use, allowing it to flush any remaining log messages before the application exits.