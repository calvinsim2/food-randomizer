using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Models;
using FunWithFoodInfrastructure.Repositories.Common;

namespace FunWithFoodInfrastructure.Repositories
{
    public class AdminRepository : BaseGenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(FoodDbContext context) : base(context) { }

        public async Task<Admin?> GetUserByUsernameAsync(string username, bool withTracking)
        {
            IQueryable<Admin> query = GetEntityQuery().Where(x => x.Username == username);

            Admin? admin = await GetByFirstOrDefaultAsync(query, withTracking);

            return admin;
        }

        public async Task<bool> IfRecordsExistInDatabaseAsync()
        {
            IQueryable<Admin> query = GetEntityQuery();

            IEnumerable<Admin> result = (await GetByListAsync(query, false)).ToList();

            return result.Any();
        }
    }
}
