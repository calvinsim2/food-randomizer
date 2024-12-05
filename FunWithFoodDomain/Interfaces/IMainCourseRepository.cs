using FunWithFoodDomain.Interfaces.Common;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Interfaces
{
    public interface IMainCourseRepository : IBaseGenericRepository<MainCourse>
    {
        Task<IEnumerable<MainCourse>> GetAllMainCourseAsync();
        Task<IEnumerable<MainCourse>> GetAllMainCourseByCuisineIdAsync(Guid cuisineId, bool withTracking);
    }
}
