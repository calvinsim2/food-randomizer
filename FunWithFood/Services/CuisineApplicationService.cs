using FunWithFood.Dto.Cuisine;
using FunWithFood.Interfaces;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Interfaces.Mappers;

namespace FunWithFood.Services
{
    public class CuisineApplicationService : ICuisineApplicationService
    {
        private readonly ICuisineService _cuisineService;
        private readonly ICuisineMapper _cuisineMapper;

        public CuisineApplicationService(ICuisineService cuisineService, ICuisineMapper cuisineMapper)
        {
            _cuisineService = cuisineService;
            _cuisineMapper = cuisineMapper;
        }

        public async Task<IEnumerable<CuisineViewModel>> GetAllCuisineViewModelAsync()
        {
            IEnumerable<CuisineDataModel> cuisineDataModels = await _cuisineService.GetAllCuisineDataModelAsync();
            IEnumerable<CuisineViewModel> cuisineViewModels = 
                            _cuisineMapper.MapCuisineDataModelsToCuisineViewModels(cuisineDataModels);

            return cuisineViewModels;
        }

        public async Task AddCuisineAsync(AddCuisineDto addCuisineDto)
        {
            CuisineDataModel cuisineDataModel = _cuisineMapper.MapAddCuisineDtoToCuisineDataModel(addCuisineDto);

            await _cuisineService.AddCuisineAsync(cuisineDataModel);
        }

        public async Task<CuisineViewModel> GetCuisineViewModelByIdAsync(Guid id)
        {
            CuisineDataModel cuisineDataModel = await _cuisineService.GetCuisineDataModelById(id);

            CuisineViewModel cuisineViewModel = _cuisineMapper.MapCuisineDataModelToCuisineViewModel(cuisineDataModel);

            return cuisineViewModel;
        }

        public async Task EditCuisineAsync(EditCuisineDto editCuisineDto)
        {
            CuisineDataModel cuisineDataModel = _cuisineMapper.MapEditCuisineDtoToCuisineDataModel(editCuisineDto);

            await _cuisineService.EditCuisineAsync(cuisineDataModel);
        }

        public async Task DeleteCuisineAsync(Guid id)
        {
            await _cuisineService.DeleteCuisineAsync(id);
        }
    }
}
