using FunWithFood.Dto.Dessert;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;

namespace FunWithFood.Interfaces.Mappers
{
    public interface IDessertMapper
    {
        DessertDataModel MapAddDessertDtoToDessertDataModel(AddDessertDto addDessertDto, byte[]? imageData);
        DessertViewModel MapDessertDataModelToDessertViewModel(DessertDataModel dessertDataModel);
        DessertDataModel MapEditDessertDtoToDessertDataModel(EditDessertDto editDessertDto, byte[]? imageData);
    }
}
