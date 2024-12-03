using FunWithFoodDomain.Common.Exceptions;
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

        public async Task<DessertDataModel> GetDessertDataModelByIdAsync(Guid id)
        {
            Dessert dessert = await _dessertRepository.GetByIdAsync(id, true) ?? throw new NotFoundException("Dessert Not Found");

            DessertDataModel dessertDataModel = _mapper.MapDessertToDessertDataModel(dessert);

            return dessertDataModel;
        }

        public async Task AddDessertAsync(DessertDataModel dessertDataModel)
        {
            Dessert dessert = _mapper.MapDessertDataModelToDessert(dessertDataModel);
            await _dessertRepository.AddAsync(dessert);
            await _dessertRepository.SaveChangesAsync();
        }

        public async Task EditDessertAsync(DessertDataModel dessertDataModel)
        {
            Dessert dessert = await _dessertRepository.GetByIdAsync(dessertDataModel.Id, true) ?? throw new NotFoundException("Dessert Not Found");

            dessert.CuisineId = dessertDataModel.CuisineId;
            dessert.Name = dessertDataModel.Name;
            dessert.ImageData = dessertDataModel.ImageData;

            _dessertRepository.Update(dessert);
            await _dessertRepository.SaveChangesAsync();
        }

        public async Task DeleteDessertAsync(Guid id)
        {
            Dessert dessert = await _dessertRepository.GetByIdAsync(id, true)
                                ?? throw new NotFoundException("Dessert Not Found");

            _dessertRepository.Remove(dessert);
            await _dessertRepository.SaveChangesAsync();

        }
    }
}
