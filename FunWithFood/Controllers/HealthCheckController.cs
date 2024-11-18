using FunWithFood.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FunWithFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly IHealthCheckApplicationService _healthCheckApplicationService;

        public HealthCheckController(IHealthCheckApplicationService healthCheckApplicationService)
        {
            _healthCheckApplicationService = healthCheckApplicationService;
        }

        [HttpGet("IsDatabaseReady")]
        public IActionResult IsDatabaseReady()
        {
            try
            {
                bool isDatabaseReady = _healthCheckApplicationService.IsDataBaseReady();
                return Ok(new { isReady = isDatabaseReady });
            }
            catch
            {
                return Ok(new { isReady = false });
            }
        }
    }
}
