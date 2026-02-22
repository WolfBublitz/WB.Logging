using System.Threading.Tasks;
using AwesomeAssertions;
using WB.Logging;

namespace LoggerTests.PropertyTests.ParentPropertyTests;

public sealed class TheParentProperty
{
    [Test]
    public async Task ShouldBeNullByDefault()
    {
        // Arrange
        await using Logger logger = new("TestLogger");

        // Act
        ILogger? parent = logger.Parent;

        // Assert
        parent.Should().BeNull();
    }

    [Test]
    public async Task ShouldReturnTheParentPassedToTheInitializer()
    {
        // Arrange
        Logger parentLogger = new("ParentLogger");
        await using Logger logger = new("ChildLogger")
        {
            Parent = parentLogger
        };

        // Act
        ILogger? parent = logger.Parent;

        // Assert
        parent.Should().BeSameAs(parentLogger);
    }
}
