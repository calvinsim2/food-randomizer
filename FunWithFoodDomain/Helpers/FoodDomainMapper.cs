using AutoMapper;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces.Mappers;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Helpers
{
    public class FoodDomainMapper : IFoodDomainMapper
    {
        private readonly IMapper _mapper;

        public FoodDomainMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<FoodDataModel> MapFoodsToFoodDataModels(IEnumerable<Food> foods)
        {
            List<FoodDataModel> foodDataModels = new List<FoodDataModel>();

            foreach (var food in foods)
            {
                FoodDataModel foodDataModel = _mapper.Map<FoodDataModel>(food);
                foodDataModels.Add(foodDataModel);
            }

            return foodDataModels;
        }

        public Food MapFoodDataModelToFood(FoodDataModel foodDataModel)
        {
            Food food = _mapper.Map<Food>(foodDataModel);
            return food;
        }

        public FoodDataModel MapFoodToFoodDataModel(Food food)
        {
            FoodDataModel foodDataModel = _mapper.Map<FoodDataModel>(food);
            return foodDataModel;
        }
    }
}
