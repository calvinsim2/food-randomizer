using AutoMapper;
using FunWithFood.Dto.Food;
using FunWithFood.Interfaces.Mappers;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;

namespace FunWithFood.Helpers
{
    public class FoodMapper : IFoodMapper
    {
        private readonly IMapper _mapper;

        public FoodMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<FoodViewModel> MapFoodDataModelsToFoodViewModels(IEnumerable<FoodDataModel> foodDataModels)
        {
            List<FoodViewModel> foodViewModels = new List<FoodViewModel>();

            foreach (var foodDataModel in foodDataModels)
            {
                FoodViewModel foodViewModel = _mapper.Map<FoodViewModel>(foodDataModel);
                foodViewModels.Add(foodViewModel);
            }

            return foodViewModels;
        }

        public FoodDataModel MapAddFoodDtoToFoodDataModel(AddFoodDto addFoodDto, byte[]? imageData)
        {
            FoodDataModel foodDataModel = _mapper.Map<FoodDataModel>(addFoodDto);
            foodDataModel.ImageData = imageData;

            return foodDataModel;
        }

        public FoodViewModel MapFoodDataModelToFoodViewModel(FoodDataModel foodDataModel)
        {
            FoodViewModel foodViewModel = _mapper.Map<FoodViewModel>(foodDataModel);

            return foodViewModel;
        }

        public FoodDataModel MapEditFoodDtoToFoodDataModel(EditFoodDto editFoodDto, byte[]? imageData)
        {
            FoodDataModel foodDataModel = _mapper.Map<FoodDataModel>(editFoodDto);
            foodDataModel.ImageData = imageData;

            return foodDataModel;
        }
    }
}
