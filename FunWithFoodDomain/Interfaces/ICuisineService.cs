using FunWithFoodDomain.DataModels;

namespace FunWithFoodDomain.Interfaces
{
    public interface ICuisineService
    {
        Task<IEnumerable<CuisineDataModel>> GetAllCuisineDataModelAsync();
        Task AddCuisineAsync(CuisineDataModel cuisineDataModel);
        Task<CuisineDataModel> GetCuisineDataModelById(Guid id);
        Task EditCuisineAsync(CuisineDataModel cuisineDataModel);
        Task DeleteCuisineAsync(Guid id);
    }
}
