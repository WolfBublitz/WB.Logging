using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using WB.Logging;

namespace LoggerTests.PropertyTests.MinimumLogLevelPropertyTests;

internal sealed class TestLogSink : ILogSink
{
    public readonly List<LogMessage> LogMessage = [];

    public void Submit(LogMessage logMessage)
        => LogMessage.Add(logMessage);
}

public sealed class TheMinimumLogLevelProperty
{
    [Test]
    public void ShouldBeInfoByDefault()
    {
        // Arrange
        Logger logger = new("TestLogger");

        // Act
        LogLevel minimumLogLevel = logger.MinimumLogLevel;

        // Assert
        minimumLogLevel.Should().Be(LogLevel.Info, because: "the default minimum log level should be LogLevel.Info.");
    }

    [Test]
    [Arguments(LogLevel.Info, 3)]
    [Arguments(LogLevel.Warning, 2)]
    [Arguments(LogLevel.Error, 1)]
    public async Task ShouldFilterLogsBelowTheMinimumLogLevel(LogLevel minimumLogLevel, int expectedLogCount)
    {
        // Arrange
        TestLogSink testLogSink = new();
        await using Logger logger = new("TestLogger")
        {
            MinimumLogLevel = minimumLogLevel
        };
        logger.AttachLogSink(testLogSink);

        // Act
        logger.Log(LogLevel.Info, "This is an info message.");
        logger.Log(LogLevel.Warning, "This is a warning message.");
        logger.Log(LogLevel.Error, "This is an error message.");
        await logger.FlushAsync().ConfigureAwait(false);

        // Assert
        testLogSink.LogMessage.Should().HaveCount(expectedLogCount, because: "only messages with a LogLevel greater than or equal to the MinimumLogLevel should be logged.");
        testLogSink.LogMessage.Should().OnlyContain(logMessage => logMessage.LogLevel >= minimumLogLevel, because: "only messages with a LogLevel greater than or equal to the MinimumLogLevel should be logged.");
    }
}