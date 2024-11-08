using FunWithFood.Dto.Food;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;

namespace FunWithFood.Interfaces.Mappers
{
    public interface IFoodMapper
    {
        IEnumerable<FoodViewModel> MapFoodDataModelsToFoodViewModels(IEnumerable<FoodDataModel> foodDataModels);
        FoodDataModel MapAddFoodDtoToFoodDataModel(AddFoodDto addFoodDto, byte[]? imageData);
        FoodViewModel MapFoodDataModelToFoodViewModel(FoodDataModel foodDataModel);
        FoodDataModel MapEditFoodDtoToFoodDataModel(EditFoodDto editFoodDto, byte[]? imageData);
    }
}
