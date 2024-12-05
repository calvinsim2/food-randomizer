using FunWithFood.Dto.MainCourse;
using FunWithFood.ViewModels;

namespace FunWithFood.Interfaces
{
    public interface IMainCourseApplicationService
    {
        Task<IEnumerable<MainCourseDisplayViewModel>> GetAllMainCourseDisplayViewModel();
        Task<MainCourseViewModel> GetMainCourseViewModelByIdAsync(Guid id);
        Task AddMainCourseAsync(AddMainCourseDto addFoodDto);
        Task EditMainCourseAsync(EditMainCourseDto editFoodDto);
        Task DeleteMainCourseAsync(Guid id);
    }
}
