namespace FunWithFoodDomain.Interfaces.Common
{
    public interface IJwtTokenHandler
    {
        bool IsTokenValid(string token);
    }
}
