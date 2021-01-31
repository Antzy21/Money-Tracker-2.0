using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Money_Tracker_2._0.Controllers
{
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
        public bool Get()
        {
            return true;
        }
    }
}
