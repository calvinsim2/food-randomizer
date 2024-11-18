using FunWithFoodDomain.Interfaces;

namespace FunWithFoodInfrastructure.Repositories
{
    public class HealthCheckRepository : IHealthCheckRepository
    {
        private readonly FoodDbContext _context;

        public HealthCheckRepository(FoodDbContext context)
        {
            _context = context;
        }

        public bool IsDataBaseReady()
        {
            bool isDatabaseAvailable = _context.Database.CanConnect();
            return isDatabaseAvailable;
        }
    }
}
