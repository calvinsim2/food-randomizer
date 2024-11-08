using FunWithFoodDomain.Interfaces.Common;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Interfaces
{
    public interface ICuisineRepository : IBaseGenericRepository<Cuisine>
    {
        Task<IEnumerable<Cuisine>> GetAllCuisineAsync();
    }
}
