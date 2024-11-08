using FunWithFood.Dto.Admin;
using FunWithFood.Interfaces;
using FunWithFood.Interfaces.Mappers;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Interfaces.Common;

namespace FunWithFood.Services
{
    public class AdminApplicationService : IAdminApplicationService
    {
        private readonly IAdminService _adminService;
        private readonly IAdminMapper _mapper;
        private readonly IAuthorizationUtilityMethods _authorizationUtilityMethods;

        public AdminApplicationService(IAdminService adminService, IAdminMapper mapper, 
                                       IAuthorizationUtilityMethods authorizationUtilityMethods)
        {
            _adminService = adminService;
            _mapper = mapper;
            _authorizationUtilityMethods = authorizationUtilityMethods;
        }

        public async Task RegisterAdminAccountAsync(AddAdminDto addAdminDto)
        {
            AddAdminDataModel addAdminDataModel = _mapper.MapAddAdminDtoToAddAdminDataModel(addAdminDto);
            await _adminService.AddAdminAccountAsync(addAdminDataModel);
        }

        public async Task<bool> CheckIfRecordsExistInDatabaseAsync()
        {
            return await _adminService.CheckIfRecordsExistInDatabaseAsync();
        }

        public async Task<string> LoginUserAsync(LoginDto loginDto)
        {
            LoginDataModel loginDataModel =
                _mapper.MapLoginDtoToLoginDataModel(loginDto);


            await _adminService.LoginAdminAsync(loginDataModel);

            string token = _authorizationUtilityMethods.CreateToken();

            return token;

        }
    }
}
