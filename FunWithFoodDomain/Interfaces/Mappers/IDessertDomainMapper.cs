using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Interfaces.Mappers
{
    public interface IDessertDomainMapper
    {
        IEnumerable<DessertDataModel> MapDessertsToDessertDataModels(IEnumerable<Dessert> desserts);
        Dessert MapDessertDataModelToDessert(DessertDataModel dessertDataModel);
        DessertDataModel MapDessertToDessertDataModel(Dessert dessert);
    }
}
