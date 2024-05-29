using Album.Api.Services;
using Album.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace Album.Api.Tests;

public class greetingServiceTests
{
    private readonly IGreetingService _greetingService;

    public greetingServiceTests()
    {
        _greetingService = new GreetingService();
    }

    [Fact]
    public void TestHelloWithName()
    {
        // Arange
        string name = "John ASP.NET";
        string expectedResponse = $"Hello John ASP.NET {Dns.GetHostName()}";

        // Act
        var result = _greetingService.GetGreeting(name);

        // Assert
        Assert.Equal(expectedResponse, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void TestWithoutName_DefaultGreeting(string name)
    {
        // Arange
        string expectedResponse = "Hello World";

        // Act
        var result = _greetingService.GetGreeting(name);

        // Assert
        Assert.Equal(expectedResponse, result);

    }
}