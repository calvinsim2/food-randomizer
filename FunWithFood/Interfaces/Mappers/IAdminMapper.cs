using FunWithFood.Dto.Admin;
using FunWithFoodDomain.DataModels;

namespace FunWithFood.Interfaces.Mappers
{
    public interface IAdminMapper
    {
        AddAdminDataModel MapAddAdminDtoToAddAdminDataModel(AddAdminDto addAdminDto);
        LoginDataModel MapLoginDtoToLoginDataModel(LoginDto loginDto);
    }
}
