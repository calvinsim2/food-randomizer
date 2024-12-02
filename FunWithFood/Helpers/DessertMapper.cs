using AutoMapper;
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

    }
}
