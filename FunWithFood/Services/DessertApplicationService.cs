using FunWithFood.Dto.Dessert;
using FunWithFood.Interfaces;
using FunWithFood.Interfaces.Mappers;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces;

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

        public async Task<DessertViewModel> GetDessertViewModelByIdAsync(Guid id)
        {
            DessertDataModel dessertDataModel = await _dessertService.GetDessertDataModelByIdAsync(id);

            DessertViewModel dessertViewModel = _dessertMapper.MapDessertDataModelToDessertViewModel(dessertDataModel);
            dessertViewModel.ImageBase64 = _imageConversionService.ConvertByteToBase64(dessertDataModel.ImageData);

            return dessertViewModel;
        }

        public async Task AddDessertAsync(AddDessertDto addDessertDto)
        {
            byte[] imageData = await _imageConversionService.ConvertFileToByteArray(addDessertDto.ImageFile);

            DessertDataModel dessertDataModel = _dessertMapper.MapAddDessertDtoToDessertDataModel(addDessertDto, imageData);
            await _dessertService.AddDessertAsync(dessertDataModel);
        }

        public async Task EditDessertAsync(EditDessertDto editDessertDto)
        {
            byte[]? imageData = editDessertDto.ImageFile is null ? _imageConversionService.ConvertBase64ToByte(editDessertDto.ImageBase64) :
                                await _imageConversionService.ConvertFileToByteArray(editDessertDto.ImageFile);

            DessertDataModel dessertDataModel = _dessertMapper.MapEditDessertDtoToDessertDataModel(editDessertDto, imageData);

            await _dessertService.EditDessertAsync(dessertDataModel);
        }

        public async Task DeleteDessertAsync(Guid id)
        {
            await _dessertService.DeleteDessertAsync(id);
        }
    }
}
