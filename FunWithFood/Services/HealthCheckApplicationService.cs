using FunWithFood.Interfaces;
using FunWithFoodDomain.Interfaces;

namespace FunWithFood.Services
{
    public class HealthCheckApplicationService : IHealthCheckApplicationService
    {
        private readonly IHealthCheckService _healthCheckService;

        public HealthCheckApplicationService(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        public bool IsDataBaseReady()
        {
            return _healthCheckService.IsDataBaseReady();
        }
    }
}
