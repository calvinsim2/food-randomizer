using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Interfaces.Mappers;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Services
{
    public class DessertService : IDessertService
    {
        private readonly IDessertRepository _dessertRepository;
        private readonly IDessertDomainMapper _mapper;
        public DessertService(IDessertRepository dessertRepository, IDessertDomainMapper mapper) 
        {
            _dessertRepository = dessertRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DessertDataModel>> GetAllDessertDataModelsAsync()
        {
            IEnumerable<Dessert> desserts = await _dessertRepository.GetAllDessertAsync();

            IEnumerable<DessertDataModel> dessertDataModels = _mapper.MapDessertsToDessertDataModels(desserts);

            return dessertDataModels;
        }
    }
}
