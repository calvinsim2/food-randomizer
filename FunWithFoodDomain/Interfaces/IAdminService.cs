using FunWithFoodDomain.DataModels;

namespace FunWithFoodDomain.Interfaces
{
    public interface IAdminService
    {
        Task AddAdminAccountAsync(AddAdminDataModel addAdminDataModel);
        Task<bool> CheckIfRecordsExistInDatabaseAsync();
        Task LoginAdminAsync(LoginDataModel loginRequestDataModel);
    }
}
