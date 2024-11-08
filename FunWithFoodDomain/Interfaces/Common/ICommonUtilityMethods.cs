namespace FunWithFoodDomain.Interfaces.Common
{
    public interface ICommonUtilityMethods
    {
        Guid ConvertStringToGuid(string stringId);
        string RemoveEmptySpaceAndCapitalizeString(string inputString);
        void ValidateStringIfIsEmptyOrNull(string inputString);
    }
}
