using FunWithFood.Dto.Dessert;
using FunWithFood.ViewModels;

namespace FunWithFood.Interfaces
{
    public interface IDessertApplicationService
    {
        Task<IEnumerable<DessertDisplayViewModel>> GetAllDessertDisplayViewModelAsync();
        Task<DessertViewModel> GetDessertViewModelByIdAsync(Guid id);
        Task AddDessertAsync(AddDessertDto addDessertDto);
        Task EditDessertAsync(EditDessertDto editDessertDto);
        Task DeleteDessertAsync(Guid id);
    }
}
