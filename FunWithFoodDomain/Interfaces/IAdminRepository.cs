using FunWithFoodDomain.Interfaces.Common;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Interfaces
{
    public interface IAdminRepository : IBaseGenericRepository<Admin>
    {
        Task<Admin?> GetUserByUsernameAsync(string username, bool withTracking);
        Task<bool> IfRecordsExistInDatabaseAsync();
    }
}
