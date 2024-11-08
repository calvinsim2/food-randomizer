using FunWithFoodDomain.Interfaces.Common;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Interfaces
{
    public interface IFoodRepository : IBaseGenericRepository<Food>
    {
        Task<IEnumerable<Food>> GetAllFoodAsync();
        Task<IEnumerable<Food>> GetAllFoodByCuisineIdAsync(Guid cuisineId, bool withTracking);
    }
}
