using FunWithFoodDomain.Common.Exceptions;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Interfaces.Mappers;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IFoodDomainMapper _foodDomainMapper;

        public FoodService(IFoodRepository foodRepository, IFoodDomainMapper foodDomainMapper)
        {
            _foodRepository = foodRepository;
            _foodDomainMapper = foodDomainMapper;
        }

        public async Task<IEnumerable<FoodDataModel>> GetAllFoodDataModelAsync()
        {
            IEnumerable<Food> foods = await _foodRepository.GetAllFoodAsync();

            IEnumerable<FoodDataModel> foodDataModels = _foodDomainMapper.MapFoodsToFoodDataModels(foods);

            return foodDataModels;
        }

        public async Task<FoodDataModel> GetFoodDataModelById(Guid id)
        {
            Food food = await _foodRepository.GetByIdAsync(id, true) ?? throw new NotFoundException("Food Not Found");

            FoodDataModel foodDataModel = _foodDomainMapper.MapFoodToFoodDataModel(food);

            return foodDataModel;
        }

        public async Task AddFoodAsync(FoodDataModel foodDataModel)
        {
            Food food = _foodDomainMapper.MapFoodDataModelToFood(foodDataModel);

            await _foodRepository.AddAsync(food);
            await _foodRepository.SaveChangesAsync();

        }

        public async Task EditFoodAsync(FoodDataModel foodDataModel)
        {
            Food food = await _foodRepository.GetByIdAsync(foodDataModel.Id, true) ?? throw new NotFoundException("Food Not Found");

            food.CuisineId = foodDataModel.CuisineId;
            food.Name = foodDataModel.Name;
            food.ImageData = foodDataModel.ImageData;

            _foodRepository.Update(food);
            await _foodRepository.SaveChangesAsync();

        }

        public async Task DeleteFoodAsync(Guid id)
        {
            Food food = await _foodRepository.GetByIdAsync(id, true)
                                ?? throw new NotFoundException("Food Not Found");

            _foodRepository.Remove(food);
            await _foodRepository.SaveChangesAsync();

        }

        public async Task DeleteFoodByCuisineIdAsync(Guid id, bool withTracking)
        {
            IEnumerable<Food> foods = await _foodRepository.GetAllFoodByCuisineIdAsync(id, withTracking);

            _foodRepository.RemoveRange(foods);
            await _foodRepository.SaveChangesAsync();
        }
    }
}
