using Album.Api.Controllers;
using Album.Api.Services;
using Album.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Album.Api.Tests;

public class HelloControllerTests
{
    private readonly Mock<IGreetingService> _mockGreetingService;
    private readonly Mock<ILogger<HelloController>> _mockLogger;
    private readonly HelloController _controller;

    public HelloControllerTests()
    {
        _mockGreetingService = new Mock<IGreetingService>();
        _mockLogger = new Mock<ILogger<HelloController>>();
        _controller = new HelloController(_mockGreetingService.Object, _mockLogger.Object);
    }

    [Fact]
    public void TestHelloWithName()
    {
        // Arrange
        var name = "John Cloud";
        var expectedResponse = "Hello John Cloud";
        _mockGreetingService.Setup(service => service.GetGreeting(name)).Returns(expectedResponse);

        // Act
        var result = _controller.Get(name) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var message = Assert.IsType<Message>(result.Value);
        Assert.Equal(expectedResponse, message.text);
        _mockGreetingService.Verify(service => service.GetGreeting(name), Times.Once);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void TestWithoutName_DefaultGreeting(string name)
    {
        // Arrange
        var expectedResponse = "Hello World";
        _mockGreetingService.Setup(service => service.GetGreeting(name)).Returns(expectedResponse);

        // Act
        var result = _controller.Get(name) as OkObjectResult;

        // Assert
        Assert.NotNull(result);

        var message = Assert.IsType<Message>(result.Value);
        Assert.Equal(expectedResponse, message.text);
    }
}