using FunWithFoodDomain.Interfaces.Common;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Interfaces
{
    public interface IDessertRepository : IBaseGenericRepository<Dessert>
    {
        Task<IEnumerable<Dessert>> GetAllDessertAsync();
        Task<IEnumerable<Dessert>> GetAllDessertByCuisineIdAsync(Guid cuisineId);
    }
}
