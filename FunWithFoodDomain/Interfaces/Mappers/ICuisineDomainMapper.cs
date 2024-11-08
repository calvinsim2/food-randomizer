using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Interfaces.Mappers
{
    public interface ICuisineDomainMapper
    {
        IEnumerable<CuisineDataModel> MapCuisinesToCuisineDataModels(IEnumerable<Cuisine> cuisines);
        Cuisine MapCuisineDataModelToCuisine(CuisineDataModel cuisineDataModel);
        CuisineDataModel MapCuisineToCuisineDataModel(Cuisine cuisine);
    }
}
