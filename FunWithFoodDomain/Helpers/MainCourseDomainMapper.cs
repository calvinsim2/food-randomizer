using AutoMapper;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces.Mappers;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Helpers
{
    public class MainCourseDomainMapper : IMainCourseDomainMapper
    {
        private readonly IMapper _mapper;

        public MainCourseDomainMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<MainCourseDataModel> MapMainCoursesToMainCourseDataModels(IEnumerable<MainCourse> mainCourses)
        {
            List<MainCourseDataModel> mainCourseDataModels = new List<MainCourseDataModel>();

            foreach (var mainCourse in mainCourses)
            {
                MainCourseDataModel cuisineDataModel = _mapper.Map<MainCourseDataModel>(mainCourse);
                mainCourseDataModels.Add(cuisineDataModel);
            }

            return mainCourseDataModels;
        }

        public MainCourse MapMainCourseDataModelToMainCourse(MainCourseDataModel mainCourseDataModel)
        {
            MainCourse mainCourse = _mapper.Map<MainCourse>(mainCourseDataModel);
            return mainCourse;
        }

        public MainCourseDataModel MapMainCourseToMainCourseDataModel(MainCourse mainCourse)
        {
            MainCourseDataModel mainCourseDataModel = _mapper.Map<MainCourseDataModel>(mainCourse);
            return mainCourseDataModel;
        }
    }
}
