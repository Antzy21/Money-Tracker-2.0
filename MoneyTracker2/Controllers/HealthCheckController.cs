using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MoneyTracker2.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthCheckController(ILogger<HealthCheckController> logger) : ControllerBase
{
    private readonly ILogger<HealthCheckController> _logger = logger;

    [HttpGet]
    public bool Get()
    {
        return true;
    }

    [HttpPost]
    public bool Post(object o)
    {
        Console.WriteLine("HttpPost success");
        return true;
    }
}