using AutoMapper;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces.Mappers;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Helpers
{
    public class DessertDomainMapper : IDessertDomainMapper
    {
        private readonly IMapper _mapper;

        public DessertDomainMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<DessertDataModel> MapDessertsToDessertDataModels(IEnumerable<Dessert> desserts)
        {
            List<DessertDataModel> dessertDataModels = new List<DessertDataModel>();

            foreach (var dessert in desserts)
            {
                DessertDataModel dessertDataModel = _mapper.Map<DessertDataModel>(dessert);
                dessertDataModels.Add(dessertDataModel);
            }

            return dessertDataModels;
        }

        public Dessert MapDessertDataModelToDessert(DessertDataModel dessertDataModel)
        {
            Dessert dessert = _mapper.Map<Dessert>(dessertDataModel);
            return dessert;
        }

        public DessertDataModel MapDessertToDessertDataModel(Dessert dessert)
        {
            DessertDataModel dessertDataModel = _mapper.Map<DessertDataModel>(dessert);
            return dessertDataModel;
        }
    }
}
