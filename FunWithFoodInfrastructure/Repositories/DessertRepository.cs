using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Models;
using FunWithFoodInfrastructure.Repositories.Common;

namespace FunWithFoodInfrastructure.Repositories
{
    public class DessertRepository : BaseGenericRepository<Dessert>, IDessertRepository
    {
        public DessertRepository(FoodDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Dessert>> GetAllDessertAsync() 
        {
            IQueryable<Dessert> query = GetEntityQuery();

            IEnumerable<Dessert> result = await GetByListAsync(query, true);

            return result;
        }

        public async Task<IEnumerable<Dessert>> GetAllDessertByCuisineIdAsync(Guid cuisineId)
        {
            IQueryable<Dessert> query = GetEntityQuery().Where( x => x.CuisineId == cuisineId);

            IEnumerable<Dessert> result = await GetByListAsync(query, true);

            return result;
        }
    }
}
