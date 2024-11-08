using AutoMapper;
using FunWithFood.Dto.Admin;
using FunWithFood.Dto.Cuisine;
using FunWithFood.Dto.Food;
using FunWithFood.ViewModels;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Models;

namespace FunWithFood.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AddCuisineDto, CuisineDataModel>();
            CreateMap<EditCuisineDto, CuisineDataModel>();
            CreateMap<CuisineDataModel, CuisineViewModel>();
            CreateMap<FoodViewModel, FoodDataModel>();
            CreateMap<FoodDataModel, FoodViewModel>();
            CreateMap<AddFoodDto, FoodDataModel>();
            CreateMap<EditFoodDto, FoodDataModel>();

            CreateMap<Cuisine, CuisineDataModel>();
            CreateMap<CuisineDataModel, Cuisine>();
            CreateMap<FoodDataModel, Food>();
            CreateMap<Food, FoodDataModel>();

            CreateMap<LoginDto, LoginDataModel>();
            CreateMap<AddAdminDto, AddAdminDataModel>();
            CreateMap<AddAdminDataModel, Admin>();

        }
    }
}
