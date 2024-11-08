using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Models;
using FunWithFoodInfrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace FunWithFoodInfrastructure.Repositories
{
    public class CuisineRepository : BaseGenericRepository<Cuisine>, ICuisineRepository
    {
        public CuisineRepository(FoodDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Cuisine>> GetAllCuisineAsync()
        {
            IQueryable<Cuisine> query = GetEntityQuery();

            IEnumerable<Cuisine> result = await query.ToListAsync();

            return result;
        }
    }
}
