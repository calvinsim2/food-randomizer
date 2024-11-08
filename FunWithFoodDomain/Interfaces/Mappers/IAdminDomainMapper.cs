using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Interfaces.Mappers
{
    public interface IAdminDomainMapper
    {
        Admin MapAddAdminDataModelToAdmin(AddAdminDataModel addAdminDataModel);
    }
}
