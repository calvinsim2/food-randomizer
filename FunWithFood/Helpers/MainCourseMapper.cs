using AutoMapper;
using FunWithFood.Dto.MainCourse;
using FunWithFood.Interfaces.Mappers;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;

namespace FunWithFood.Helpers
{
    public class MainCourseMapper : IMainCourseMapper
    {
        private readonly IMapper _mapper;

        public MainCourseMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<MainCourseViewModel> MapMainCourseDataModelsToMainCourseViewModels(IEnumerable<MainCourseDataModel> mainCourseDataModels)
        {
            List<MainCourseViewModel> mainCourseViewModels = new List<MainCourseViewModel>();

            foreach (var mainCourseDataModel in mainCourseDataModels)
            {
                MainCourseViewModel mainCourseViewModel = _mapper.Map<MainCourseViewModel>(mainCourseDataModel);
                mainCourseViewModels.Add(mainCourseViewModel);
            }

            return mainCourseViewModels;
        }

        public MainCourseDataModel MapAddMainCourseDtoToMainCourseDataModel(AddMainCourseDto addMainCourseDto, byte[]? imageData)
        {
            MainCourseDataModel mainCourseDataModel = _mapper.Map<MainCourseDataModel>(addMainCourseDto);
            mainCourseDataModel.ImageData = imageData;

            return mainCourseDataModel;
        }

        public MainCourseViewModel MapMainCourseDataModelToMainCourseViewModel(MainCourseDataModel mainCourseDataModel)
        {
            MainCourseViewModel mainCourseViewModel = _mapper.Map<MainCourseViewModel>(mainCourseDataModel);

            return mainCourseViewModel;
        }

        public MainCourseDataModel MapEditMainCourseDtoToMainCourseDataModel(EditMainCourseDto editMainCourseDto, byte[]? imageData)
        {
            MainCourseDataModel mainCourseDataModel = _mapper.Map<MainCourseDataModel>(editMainCourseDto);
            mainCourseDataModel.ImageData = imageData;

            return mainCourseDataModel;
        }
    }
}
