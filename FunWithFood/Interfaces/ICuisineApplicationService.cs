using FunWithFood.Dto.Cuisine;
using FunWithFood.ViewModels;

namespace FunWithFood.Interfaces
{
    public interface ICuisineApplicationService
    {
        Task<IEnumerable<CuisineViewModel>> GetAllCuisineViewModelAsync();
        Task AddCuisineAsync(AddCuisineDto addCuisineDto);
        Task<CuisineViewModel> GetCuisineViewModelByIdAsync(Guid id);
        Task EditCuisineAsync(EditCuisineDto editCuisineDto);
        Task DeleteCuisineAsync(Guid id);
    }
}
