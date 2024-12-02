using FunWithFood.Interfaces;
using FunWithFood.Interfaces.Mappers;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Services;

namespace FunWithFood.Services
{
    public class DessertApplicationService : IDessertApplicationService
    {
        private readonly IDessertService _dessertService;
        private readonly ICuisineService _cuisineService;
        private readonly IDessertMapper _dessertMapper;
        private readonly IImageConversionService _imageConversionService;

        public DessertApplicationService(IDessertService dessertService, ICuisineService cuisineService, 
                                         IImageConversionService imageConversionService, IDessertMapper dessertMapper)
        {
            _dessertService = dessertService;
            _cuisineService = cuisineService;
            _imageConversionService = imageConversionService;
            _dessertMapper = dessertMapper;
        }

        public async Task<IEnumerable<DessertDisplayViewModel>> GetAllDessertDisplayViewModelAsync()
        {
            IEnumerable<DessertDataModel> dessertDataModels = await _dessertService.GetAllDessertDataModelsAsync();
            IEnumerable<CuisineDataModel> cuisineDataModels = await _cuisineService.GetAllCuisineDataModelAsync();

            List<DessertDisplayViewModel> dessertDisplayViewModels = dessertDataModels
            .Join(cuisineDataModels, dessert => dessert.CuisineId, cuisine => cuisine.Id,
            (dessert, cuisine) => new DessertDisplayViewModel
            {
                Id = dessert.Id,
                Name = dessert.Name,
                CuisineType = cuisine.Type,
                ImageBase64 = _imageConversionService.ConvertByteToBase64(dessert.ImageData)
            }).ToList();

            return dessertDisplayViewModels;
        }
    }
}
