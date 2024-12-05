using FunWithFoodDomain.Common.Exceptions;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Interfaces.Mappers;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Services
{
    public class CuisineService : ICuisineService
    {
        private readonly ICuisineRepository _cuisineRepository;
        private readonly ICuisineDomainMapper _cuisineDomainMapper;
        private readonly IMainCourseService _foodService;

        public CuisineService(ICuisineRepository cuisineRepository, ICuisineDomainMapper cuisineMapper, IMainCourseService foodService)
        {
            _cuisineRepository = cuisineRepository;
            _cuisineDomainMapper = cuisineMapper;
            _foodService = foodService;
        }

        public async Task<IEnumerable<CuisineDataModel>> GetAllCuisineDataModelAsync()
        {
            IEnumerable<Cuisine> cuisines = await _cuisineRepository.GetAllCuisineAsync();

            IEnumerable<CuisineDataModel> cuisineDataModels = _cuisineDomainMapper.MapCuisinesToCuisineDataModels(cuisines);

            return cuisineDataModels;
        }

        public async Task<CuisineDataModel> GetCuisineDataModelById(Guid id)
        {
            Cuisine cuisine = await _cuisineRepository.GetByIdAsync(id, true) ?? throw new NotFoundException("Cuisine Not Found");

            CuisineDataModel cuisineDataModel = _cuisineDomainMapper.MapCuisineToCuisineDataModel(cuisine);

            return cuisineDataModel;
        }
        public async Task AddCuisineAsync(CuisineDataModel cuisineDataModel)
        {
            Cuisine cuisine = _cuisineDomainMapper.MapCuisineDataModelToCuisine(cuisineDataModel);

            await _cuisineRepository.AddAsync(cuisine);
            await _cuisineRepository.SaveChangesAsync();

        }

        public async Task EditCuisineAsync(CuisineDataModel cuisineDataModel)
        {
            Cuisine cuisine = await _cuisineRepository.GetByIdAsync(cuisineDataModel.Id, true) 
                                ?? throw new NotFoundException("Cuisine Not Found");

            cuisine.Type = cuisineDataModel.Type;

            _cuisineRepository.Update(cuisine);
            await _cuisineRepository.SaveChangesAsync();

        }

        public async Task DeleteCuisineAsync(Guid id)
        {
            Cuisine cuisine = await _cuisineRepository.GetByIdAsync(id, true)
                                ?? throw new NotFoundException("Cuisine Not Found");

            await _foodService.DeleteMainCourseByCuisineIdAsync(id, true);

            _cuisineRepository.Remove(cuisine);
            await _cuisineRepository.SaveChangesAsync();

        }
    }
}
