using FunWithFood.Dto.MainCourse;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;

namespace FunWithFood.Interfaces.Mappers
{
    public interface IMainCourseMapper
    {
        IEnumerable<MainCourseViewModel> MapMainCourseDataModelsToMainCourseViewModels(IEnumerable<MainCourseDataModel> mainCourseDataModels);
        MainCourseDataModel MapAddMainCourseDtoToMainCourseDataModel(AddMainCourseDto addMainCourseDto, byte[]? imageData);
        MainCourseViewModel MapMainCourseDataModelToMainCourseViewModel(MainCourseDataModel mainCourseDataModel);
        MainCourseDataModel MapEditMainCourseDtoToMainCourseDataModel(EditMainCourseDto editMainCourseDto, byte[]? imageData);
    }
}
