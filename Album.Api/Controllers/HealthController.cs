using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

[ApiController]
[Route("[controller]")]
public class HealthCheckController : ControllerBase
{
    private readonly ILogger<HealthCheckController> _logger;

    public HealthCheckController(ILogger<HealthCheckController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("/health")]
    public async Task<IActionResult> CheckHealth()
    {
        try
        {
            return Ok("Healthy");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Health check failed: {ex.Message}");
            return StatusCode(500, "Unhealthy");
        }
    }
}
