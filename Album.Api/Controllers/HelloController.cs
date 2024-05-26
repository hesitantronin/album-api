using Album.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Album.Api.Models;

namespace Album.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class HelloController : ControllerBase
    {

        private readonly IGreetingService _greetingService;
        private readonly ILogger<HelloController> _logger;

        public HelloController(IGreetingService greetingService, ILogger<HelloController> logger)
        {
            _greetingService = greetingService;
            _logger = logger;
        }

        [HttpGet("hello")]
        public IActionResult Get([FromQuery] string name = null)
        {        
            var response = _greetingService.GetGreeting(name);
            _logger.LogCritical(name);
            return Ok(new Message { text = response });
        }
    }
}