using FunWithFood.Dto.Food;
using FunWithFood.Interfaces;
using FunWithFood.Interfaces.Mappers;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces;

namespace FunWithFood.Services
{
    public class FoodApplicationService : IFoodApplicationService
    {
        private readonly IFoodService _foodService;
        private readonly ICuisineService _cuisineService;
        private readonly IImageConversionService _imageConversionService;
        private readonly IFoodMapper _foodMapper;

        public FoodApplicationService(IFoodService foodService,
                                      ICuisineService cuisineService,
                                      IFoodMapper foodMapper,
                                      IImageConversionService imageConversionService)
        {
            _foodService = foodService;
            _cuisineService = cuisineService;
            _foodMapper = foodMapper;
            _imageConversionService = imageConversionService;
        }

        public async Task<IEnumerable<FoodDisplayViewModel>> GetAllFoodDisplayViewModel()
        {
            IEnumerable<FoodDataModel> foodDataModels = await _foodService.GetAllFoodDataModelAsync();
            IEnumerable<CuisineDataModel> cuisineDataModels = await _cuisineService.GetAllCuisineDataModelAsync();

            List<FoodDisplayViewModel> foodDisplayViewModels = foodDataModels
            .Join(cuisineDataModels, food => food.CuisineId, cuisine => cuisine.Id,
            (food, cuisine) => new FoodDisplayViewModel
            {
                Id = food.Id,
                Name = food.Name,
                CuisineType = cuisine.Type,
                ImageBase64 = _imageConversionService.ConvertByteToBase64(food.ImageData)
            }).ToList();

            return foodDisplayViewModels;
        }

        public async Task<FoodViewModel> GetFoodViewModelByIdAsync(Guid id)
        {
            FoodDataModel foodDataModel = await _foodService.GetFoodDataModelById(id);

            FoodViewModel foodViewModel = _foodMapper.MapFoodDataModelToFoodViewModel(foodDataModel);
            foodViewModel.ImageBase64 = _imageConversionService.ConvertByteToBase64(foodDataModel.ImageData);

            return foodViewModel;
        }

        public async Task AddFoodAsync(AddFoodDto addFoodDto)
        {
            byte[]? imageData = await _imageConversionService.ConvertFileToByteArray(addFoodDto.ImageFile);

            FoodDataModel foodDataModel = _foodMapper.MapAddFoodDtoToFoodDataModel(addFoodDto, imageData);
            await _foodService.AddFoodAsync(foodDataModel);
        }

        public async Task EditFoodAsync(EditFoodDto editFoodDto)
        {
            byte[]? imageData = await _imageConversionService.ConvertFileToByteArray(editFoodDto.ImageFile);

            FoodDataModel foodDataModel = _foodMapper.MapEditFoodDtoToFoodDataModel(editFoodDto, imageData);

            await _foodService.EditFoodAsync(foodDataModel);
        }

        public async Task DeleteFoodAsync(Guid id)
        {
            await _foodService.DeleteFoodAsync(id);
        }
    }
}
