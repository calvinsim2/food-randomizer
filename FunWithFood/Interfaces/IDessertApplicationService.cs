using FunWithFood.ViewModels;

namespace FunWithFood.Interfaces
{
    public interface IDessertApplicationService
    {
        Task<IEnumerable<DessertDisplayViewModel>> GetAllDessertDisplayViewModelAsync();
    }
}
