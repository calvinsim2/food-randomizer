using FunWithFoodDomain.DataModels;

namespace FunWithFoodDomain.Interfaces
{
    public interface IDessertService
    {
        Task<IEnumerable<DessertDataModel>> GetAllDessertDataModelsAsync();
    }
}
