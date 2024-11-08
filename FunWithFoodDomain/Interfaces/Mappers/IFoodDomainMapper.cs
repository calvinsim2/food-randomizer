using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Interfaces.Mappers
{
    public interface IFoodDomainMapper
    {
        IEnumerable<FoodDataModel> MapFoodsToFoodDataModels(IEnumerable<Food> foods);
        Food MapFoodDataModelToFood(FoodDataModel foodDataModel);
        FoodDataModel MapFoodToFoodDataModel(Food food);
    }
}
