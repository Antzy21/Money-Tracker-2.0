using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MoneyTracker2.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthCheckController(ILogger<HealthCheckController> logger) : ControllerBase
{
    private readonly ILogger<HealthCheckController> _logger = logger;

    [HttpGet]
    public IActionResult Get() => Ok();
}