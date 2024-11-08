using FunWithFoodDomain.DataModels;

namespace FunWithFoodDomain.Interfaces
{
    public interface IFoodService
    {
        Task<IEnumerable<FoodDataModel>> GetAllFoodDataModelAsync();
        Task<FoodDataModel> GetFoodDataModelById(Guid id);
        Task AddFoodAsync(FoodDataModel foodDataModel);
        Task EditFoodAsync(FoodDataModel foodDataModel);
        Task DeleteFoodAsync(Guid id);
        Task DeleteFoodByCuisineIdAsync(Guid id, bool withTracking);
    }
}
