using System;
using System.Threading.Tasks;
using AwesomeAssertions;
using WB.Logging;

namespace ExtensionsTests.AttachConsoleLogSinkMethodTests;

public sealed class TheAttachConsoleMethod
{
    [Test]
    public async Task ShouldAttachAConsoleLogSinkToTheLogger()
    {
        // Arrange
        using TestConsole testConsole = new();
        ILogger logger = new Logger("Test");

        // Act
        logger.AttachConsole();
        logger.Info("Log sink is attached.");
        await logger.FlushAsync().ConfigureAwait(false);

        // Assert
        string output = testConsole.Output;
        output.Should().Contain("Log sink is attached.");
    }

    [Test]
    public async Task ShouldReturnAnIDisposableThatDetachesTheConsoleLogSink()
    {
        // Arrange
        using TestConsole testConsole = new();
        ILogger logger = new Logger("Test");
        IDisposable disposable = logger.AttachConsole();

        // Act
        disposable.Dispose();
        logger.Info("Log sink is attached.");
        await logger.FlushAsync().ConfigureAwait(false);

        // Assert
        string output = testConsole.Output;
        output.Should().BeEmpty();
    }
}