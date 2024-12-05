using AutoMapper;
using FunWithFood.Dto.Admin;
using FunWithFood.Dto.Cuisine;
using FunWithFood.Dto.MainCourse;
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
            CreateMap<MainCourseViewModel, MainCourseDataModel>();
            CreateMap<MainCourseDataModel, MainCourseViewModel>();
            CreateMap<AddMainCourseDto, MainCourseDataModel>();
            CreateMap<EditMainCourseDto, MainCourseDataModel>();

            CreateMap<Cuisine, CuisineDataModel>();
            CreateMap<CuisineDataModel, Cuisine>();
            CreateMap<MainCourseDataModel, MainCourse>();
            CreateMap<MainCourse, MainCourseDataModel>();

            CreateMap<LoginDto, LoginDataModel>();
            CreateMap<AddAdminDto, AddAdminDataModel>();
            CreateMap<AddAdminDataModel, Admin>();

        }
    }
}
