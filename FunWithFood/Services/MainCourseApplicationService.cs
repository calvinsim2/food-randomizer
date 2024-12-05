using FunWithFood.Dto.MainCourse;
using FunWithFood.Interfaces;
using FunWithFood.Interfaces.Mappers;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces;

namespace FunWithFood.Services
{
    public class MainCourseApplicationService : IMainCourseApplicationService
    {
        private readonly IMainCourseService _mainCourseService;
        private readonly ICuisineService _cuisineService;
        private readonly IImageConversionService _imageConversionService;
        private readonly IMainCourseMapper _mainCourseMapper;

        public MainCourseApplicationService(IMainCourseService mainCourseService,
                                      ICuisineService cuisineService,
                                      IMainCourseMapper mainCourseMapper,
                                      IImageConversionService imageConversionService)
        {
            _mainCourseService = mainCourseService;
            _cuisineService = cuisineService;
            _mainCourseMapper = mainCourseMapper;
            _imageConversionService = imageConversionService;
        }

        public async Task<IEnumerable<MainCourseDisplayViewModel>> GetAllMainCourseDisplayViewModel()
        {
            IEnumerable<MainCourseDataModel> mainCourseDataModels = await _mainCourseService.GetAllMainCourseDataModelAsync();
            IEnumerable<CuisineDataModel> cuisineDataModels = await _cuisineService.GetAllCuisineDataModelAsync();

            List<MainCourseDisplayViewModel> mainCourseDisplayViewModels = mainCourseDataModels
            .Join(cuisineDataModels, mainCourse => mainCourse.CuisineId, cuisine => cuisine.Id,
            (mainCourse, cuisine) => new MainCourseDisplayViewModel
            {
                Id = mainCourse.Id,
                Name = mainCourse.Name,
                CuisineType = cuisine.Type,
                ImageBase64 = _imageConversionService.ConvertByteToBase64(mainCourse.ImageData)
            }).ToList();

            return mainCourseDisplayViewModels;
        }

        public async Task<MainCourseViewModel> GetMainCourseViewModelByIdAsync(Guid id)
        {
            MainCourseDataModel mainCourseDataModel = await _mainCourseService.GetMainCourseDataModelByIdAsync(id);

            MainCourseViewModel mainCourseViewModel = _mainCourseMapper.MapMainCourseDataModelToMainCourseViewModel(mainCourseDataModel);
            mainCourseViewModel.ImageBase64 = _imageConversionService.ConvertByteToBase64(mainCourseDataModel.ImageData);

            return mainCourseViewModel;
        }

        public async Task AddMainCourseAsync(AddMainCourseDto addMainCourseDto)
        {
            byte[] imageData = await _imageConversionService.ConvertFileToByteArray(addMainCourseDto.ImageFile);

            MainCourseDataModel mainCourseDataModel = _mainCourseMapper.MapAddMainCourseDtoToMainCourseDataModel(addMainCourseDto, imageData);
            await _mainCourseService.AddMainCourseAsync(mainCourseDataModel);
        }

        public async Task EditMainCourseAsync(EditMainCourseDto editMainCourseDto)
        {
            byte[]? imageData = editMainCourseDto.ImageFile is null ? _imageConversionService.ConvertBase64ToByte(editMainCourseDto.ImageBase64) :
                                await _imageConversionService.ConvertFileToByteArray(editMainCourseDto.ImageFile);

            MainCourseDataModel mainCourseDataModel = _mainCourseMapper.MapEditMainCourseDtoToMainCourseDataModel(editMainCourseDto, imageData);

            await _mainCourseService.EditMainCourseAsync(mainCourseDataModel);
        }

        public async Task DeleteMainCourseAsync(Guid id)
        {
            await _mainCourseService.DeleteMainCourseAsync(id);
        }
    }
}
