using AutoMapper;
using FunWithFood.Dto.Cuisine;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces.Mappers;

namespace FunWithFood.Helpers
{
    public class CuisineMapper : ICuisineMapper
    {
        private readonly IMapper _mapper;

        public CuisineMapper(IMapper mapper) 
        {
            _mapper = mapper;
        }

        public IEnumerable<CuisineViewModel> MapCuisineDataModelsToCuisineViewModels(IEnumerable<CuisineDataModel> cuisineDataModels)
        {
            List<CuisineViewModel> cuisineViewModels = new List<CuisineViewModel>();

            foreach (var cuisineDataModel in cuisineDataModels)
            {
                CuisineViewModel cuisineViewModel = _mapper.Map<CuisineViewModel>(cuisineDataModel);
                cuisineViewModels.Add(cuisineViewModel);
            }

            return cuisineViewModels;
        }

        public CuisineDataModel MapAddCuisineDtoToCuisineDataModel(AddCuisineDto cuisineDto)
        {
            CuisineDataModel cuisineDataModel = _mapper.Map<CuisineDataModel>(cuisineDto);

            return cuisineDataModel;
        }

        public CuisineViewModel MapCuisineDataModelToCuisineViewModel(CuisineDataModel cuisineDataModel)
        {
            CuisineViewModel cuisineViewModel = _mapper.Map<CuisineViewModel>(cuisineDataModel);

            return cuisineViewModel;
        }

        public CuisineDataModel MapEditCuisineDtoToCuisineDataModel(EditCuisineDto editCuisineDto)
        {
            CuisineDataModel cuisineDataModel = _mapper.Map<CuisineDataModel>(editCuisineDto);

            return cuisineDataModel;
        }
    }
}
