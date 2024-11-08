namespace FunWithFoodDomain.Interfaces.Common
{
    public interface IAuthorizationUtilityMethods
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateToken();
    }
}
