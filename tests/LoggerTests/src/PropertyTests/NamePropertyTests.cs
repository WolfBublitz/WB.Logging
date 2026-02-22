using System.Threading.Tasks;
using AwesomeAssertions;
using WB.Logging;

namespace LoggerTests.PropertyTests.NamePropertyTests;

public sealed class TheNameProperty
{
    [Test]
    public async Task ShouldReturnTheNamePassedToTheConstructor()
    {
        // Arrange
        const string expectedName = "TestLogger";

        // Act
        await using Logger logger = new(expectedName);

        // Assert
        logger.Name.Should().Be(expectedName);
    }
}