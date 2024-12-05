using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Models;
using FunWithFoodInfrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace FunWithFoodInfrastructure.Repositories
{
    public class MainCourseRepository : BaseGenericRepository<MainCourse>, IMainCourseRepository
    {
        public MainCourseRepository(FoodDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<MainCourse>> GetAllMainCourseAsync()
        {
            IQueryable<MainCourse> query = GetEntityQuery();

            IEnumerable<MainCourse> result = await query.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<MainCourse>> GetAllMainCourseByCuisineIdAsync(Guid cuisineId, bool withTracking)
        {
            IQueryable<MainCourse> query = GetEntityQuery().Where(x => x.CuisineId == cuisineId);

            IEnumerable<MainCourse> result = await GetByListAsync(query, withTracking);

            return result;
        }
    }
}
