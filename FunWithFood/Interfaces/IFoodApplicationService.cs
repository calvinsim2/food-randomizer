using FunWithFood.Dto.Food;
using FunWithFood.ViewModels;

namespace FunWithFood.Interfaces
{
    public interface IFoodApplicationService
    {
        Task<IEnumerable<FoodDisplayViewModel>> GetAllFoodDisplayViewModel();
        Task<FoodViewModel> GetFoodViewModelByIdAsync(Guid id);
        Task AddFoodAsync(AddFoodDto addFoodDto);
        Task EditFoodAsync(EditFoodDto editFoodDto);
        Task DeleteFoodAsync(Guid id);
    }
}
