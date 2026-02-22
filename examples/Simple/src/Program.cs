using System;
using WB.Logging;

await using ILogger logger = new Logger("SimpleLogger");

logger.AttachConsole();

logger.Info("This is an info message.");
logger.Warning("This is a warning message.");
logger.Error("This is an error message +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.");

try
{
    throw new InvalidOperationException("This is an exception message.");
}
catch (Exception ex)
{
    logger.Log(ex);
}
