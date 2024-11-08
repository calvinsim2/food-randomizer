using FunWithFood.Dto.Cuisine;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;

namespace FunWithFoodDomain.Interfaces.Mappers
{
    public interface ICuisineMapper
    {
        IEnumerable<CuisineViewModel> MapCuisineDataModelsToCuisineViewModels(IEnumerable<CuisineDataModel> cuisineDataModels);
        CuisineDataModel MapAddCuisineDtoToCuisineDataModel(AddCuisineDto cuisineDto);
        CuisineViewModel MapCuisineDataModelToCuisineViewModel(CuisineDataModel cuisineDataModel);
        CuisineDataModel MapEditCuisineDtoToCuisineDataModel(EditCuisineDto editCuisineDto);

    }
}
