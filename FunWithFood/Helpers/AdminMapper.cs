using AutoMapper;
using FunWithFood.Dto.Admin;
using FunWithFood.Interfaces.Mappers;
using FunWithFoodDomain.DataModels;

namespace FunWithFood.Helpers
{
    public class AdminMapper : IAdminMapper
    {
        private readonly IMapper _mapper;

        public AdminMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public AddAdminDataModel MapAddAdminDtoToAddAdminDataModel(AddAdminDto addAdminDto)
        {
            AddAdminDataModel addAdminDataModel = _mapper.Map<AddAdminDataModel>(addAdminDto);

            return addAdminDataModel;
        }
        public LoginDataModel MapLoginDtoToLoginDataModel(LoginDto loginDto)
        {
            LoginDataModel loginDataModel = _mapper.Map<LoginDataModel>(loginDto);

            return loginDataModel;
        }
    }
}
