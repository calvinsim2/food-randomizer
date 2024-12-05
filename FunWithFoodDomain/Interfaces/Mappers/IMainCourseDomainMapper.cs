using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Interfaces.Mappers
{
    public interface IMainCourseDomainMapper
    {
        IEnumerable<MainCourseDataModel> MapMainCoursesToMainCourseDataModels(IEnumerable<MainCourse> mainCourses);
        MainCourse MapMainCourseDataModelToMainCourse(MainCourseDataModel mainCourseDataModel);
        MainCourseDataModel MapMainCourseToMainCourseDataModel(MainCourse mainCourse);
    }
}
