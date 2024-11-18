using FunWithFoodDomain.Interfaces;

namespace FunWithFoodDomain.Services
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly IHealthCheckRepository _healthCheckRepository;

        public HealthCheckService(IHealthCheckRepository healthCheckRepository)
        {
            _healthCheckRepository = healthCheckRepository;
        }

        public bool IsDataBaseReady()
        {
            return _healthCheckRepository.IsDataBaseReady();
        }
    }
}
