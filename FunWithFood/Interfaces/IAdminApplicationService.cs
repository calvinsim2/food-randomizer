using FunWithFood.Dto.Admin;

namespace FunWithFood.Interfaces
{
    public interface IAdminApplicationService
    {
        Task RegisterAdminAccountAsync(AddAdminDto addAdminDto);
        Task<bool> CheckIfRecordsExistInDatabaseAsync();
        Task<string> LoginUserAsync(LoginDto loginDto);
    }
}
