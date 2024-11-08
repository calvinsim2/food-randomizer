using FunWithFoodDomain.Common.Exceptions;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Interfaces.Common;
using FunWithFoodDomain.Interfaces.Mappers;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthorizationUtilityMethods _authorizationUtilityMethods;
        private readonly IAdminDomainMapper _adminDomainMapper;
        public AdminService(IAdminRepository adminRepository, IAuthorizationUtilityMethods authorizationUtilityMethods,
                            IAdminDomainMapper adminDomainMapper) 
        {
            _adminRepository = adminRepository;
            _authorizationUtilityMethods = authorizationUtilityMethods;
            _adminDomainMapper = adminDomainMapper;
        }

        public async Task AddAdminAccountAsync(AddAdminDataModel addAdminDataModel)
        {
            Admin admin = _adminDomainMapper.MapAddAdminDataModelToAdmin(addAdminDataModel);

            _authorizationUtilityMethods.CreatePasswordHash(addAdminDataModel.Password, out byte[] passwordHash, out byte[] passwordSalt);
            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;

            await _adminRepository.AddAsync(admin);
            await _adminRepository.SaveChangesAsync();
        }

        public async Task<bool> CheckIfRecordsExistInDatabaseAsync()
        {
            return await _adminRepository.IfRecordsExistInDatabaseAsync();
        }
        public async Task LoginAdminAsync(LoginDataModel loginRequestDataModel)
        {
            Admin? existingAdminAccount = await _adminRepository.GetUserByUsernameAsync(loginRequestDataModel.Username, false);

            if (existingAdminAccount is null)
            {
                throw new NotFoundException("Account not Found");
            }

            bool passwordMatches =
                _authorizationUtilityMethods.VerifyPasswordHash(loginRequestDataModel.Password, existingAdminAccount.PasswordHash, existingAdminAccount.PasswordSalt);

            if (!passwordMatches)
            {
                throw new BadRequestException("Incorrect password provided.");
            }

        }
    }
}
