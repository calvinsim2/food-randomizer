using AutoMapper;
using FunWithFood.Dto.Dessert;
using FunWithFood.Interfaces.Mappers;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;

namespace FunWithFood.Helpers
{
    public class DessertMapper : IDessertMapper
    {
        private readonly IMapper _mapper;

        public DessertMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public DessertDataModel MapAddDessertDtoToDessertDataModel(AddDessertDto addDessertDto, byte[]? imageData)
        {
            DessertDataModel dessertDataModel = _mapper.Map<DessertDataModel>(addDessertDto);
            dessertDataModel.ImageData = imageData;

            return dessertDataModel;
        }

        public DessertViewModel MapDessertDataModelToDessertViewModel(DessertDataModel dessertDataModel)
        {
            DessertViewModel dessertViewModel = _mapper.Map<DessertViewModel>(dessertDataModel);

            return dessertViewModel;
        }

        public DessertDataModel MapEditDessertDtoToDessertDataModel(EditDessertDto editDessertDto, byte[]? imageData)
        {
            DessertDataModel dessertDataModel = _mapper.Map<DessertDataModel>(editDessertDto);
            dessertDataModel.ImageData = imageData;

            return dessertDataModel;
        }

    }
}
