using AutoMapper;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces.Mappers;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Helpers
{
    public class CuisineDomainMapper : ICuisineDomainMapper
    {
        private readonly IMapper _mapper;

        public CuisineDomainMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<CuisineDataModel> MapCuisinesToCuisineDataModels(IEnumerable<Cuisine> cuisines)
        {
            List<CuisineDataModel> cuisineDataModels = new List<CuisineDataModel>();

            foreach (var cuisine in cuisines)
            {
                CuisineDataModel cuisineDataModel = _mapper.Map<CuisineDataModel>(cuisine);
                cuisineDataModels.Add(cuisineDataModel);
            }

            return cuisineDataModels;
        }

        public Cuisine MapCuisineDataModelToCuisine(CuisineDataModel cuisineDataModel)
        {
            Cuisine cuisine = _mapper.Map<Cuisine>(cuisineDataModel);
            return cuisine;
        }

        public CuisineDataModel MapCuisineToCuisineDataModel(Cuisine cuisine)
        {
            CuisineDataModel cuisineDataModel = _mapper.Map<CuisineDataModel>(cuisine);
            return cuisineDataModel;
        }
    }
}
