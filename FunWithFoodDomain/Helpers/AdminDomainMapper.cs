using AutoMapper;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces.Mappers;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Helpers
{
    public class AdminDomainMapper : IAdminDomainMapper
    {
        private readonly IMapper _mapper;

        public AdminDomainMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Admin MapAddAdminDataModelToAdmin(AddAdminDataModel addAdminDataModel)
        {
            Admin admin = _mapper.Map<Admin>(addAdminDataModel);

            return admin;
        }
    }
}
