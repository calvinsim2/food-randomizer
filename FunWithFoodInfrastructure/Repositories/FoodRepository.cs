using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Models;
using FunWithFoodInfrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace FunWithFoodInfrastructure.Repositories
{
    public class FoodRepository : BaseGenericRepository<Food>, IFoodRepository
    {
        public FoodRepository(FoodDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Food>> GetAllFoodAsync()
        {
            IQueryable<Food> query = GetEntityQuery();

            IEnumerable<Food> result = await query.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Food>> GetAllFoodByCuisineIdAsync(Guid cuisineId, bool withTracking)
        {
            IQueryable<Food> query = GetEntityQuery().Where(x => x.CuisineId == cuisineId);

            IEnumerable<Food> result = await GetByListAsync(query, withTracking);

            return result;
        }
    }
}
