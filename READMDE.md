# WB.Logging

An easy-to-use non-blocking logging library for .NET applications, designed to provide a simple and efficient way to log messages with various levels of severity.

## Example

```csharp
using WB.Logging;

await using ILogger logger = new Logger("MyApp");

logger.LogInfo("This is an informational message.");
logger.LogWarning("This is a warning message.");
logger.LogError("This is an error message.");
```
