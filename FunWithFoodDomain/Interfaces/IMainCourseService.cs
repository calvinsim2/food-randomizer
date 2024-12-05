using FunWithFoodDomain.DataModels;

namespace FunWithFoodDomain.Interfaces
{
    public interface IMainCourseService
    {
        Task<IEnumerable<MainCourseDataModel>> GetAllMainCourseDataModelAsync();
        Task<MainCourseDataModel> GetMainCourseDataModelByIdAsync(Guid id);
        Task AddMainCourseAsync(MainCourseDataModel mainCourseDataModel);
        Task EditMainCourseAsync(MainCourseDataModel mainCourseDataModel);
        Task DeleteMainCourseAsync(Guid id);
        Task DeleteMainCourseByCuisineIdAsync(Guid id, bool withTracking);
    }
}
