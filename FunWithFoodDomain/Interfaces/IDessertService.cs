using FunWithFoodDomain.DataModels;

namespace FunWithFoodDomain.Interfaces
{
    public interface IDessertService
    {
        Task<IEnumerable<DessertDataModel>> GetAllDessertDataModelsAsync();
        Task<DessertDataModel> GetDessertDataModelByIdAsync(Guid id);
        Task AddDessertAsync(DessertDataModel dessertDataModel);
        Task EditDessertAsync(DessertDataModel dessertDataModel);
        Task DeleteDessertAsync(Guid id);
    }
}
