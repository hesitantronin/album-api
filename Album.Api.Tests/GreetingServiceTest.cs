using Album.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Album.Api.Tests;

public class greetingServiceTests
{
    private readonly GreetingService _greetingService;

    public greetingServiceTests()
    {
        _greetingService = new GreetingService();
    }

    [Fact]
    public void TestHello()
    {
        var mockGreetingService = new Mock<IGreetingService>();
        mockGreetingService.Setup(service => service.GetGreeting("")).Returns("Bingbong dingdong");

        var testGreeting = (mockGreetingService.Object);
    }
}