using FunWithFoodDomain.Common.Exceptions;
using FunWithFoodDomain.DataModels;
using FunWithFoodDomain.Interfaces;
using FunWithFoodDomain.Interfaces.Mappers;
using FunWithFoodDomain.Models;

namespace FunWithFoodDomain.Services
{
    public class MainCourseService : IMainCourseService
    {
        private readonly IMainCourseRepository _mainCourseRepository;
        private readonly IMainCourseDomainMapper _mainCourseDomainMapper;

        public MainCourseService(IMainCourseRepository mainCourseRepository, IMainCourseDomainMapper mainCourseDomainMapper)
        {
            _mainCourseRepository = mainCourseRepository;
            _mainCourseDomainMapper = mainCourseDomainMapper;
        }

        public async Task<IEnumerable<MainCourseDataModel>> GetAllMainCourseDataModelAsync()
        {
            IEnumerable<MainCourse> mainCourses = await _mainCourseRepository.GetAllMainCourseAsync();

            IEnumerable<MainCourseDataModel> mainCourseDataModels = _mainCourseDomainMapper.MapMainCoursesToMainCourseDataModels(mainCourses);

            return mainCourseDataModels;
        }

        public async Task<MainCourseDataModel> GetMainCourseDataModelByIdAsync(Guid id)
        {
            MainCourse mainCourse = await _mainCourseRepository.GetByIdAsync(id, true) ?? throw new NotFoundException("Main Course Not Found");

            MainCourseDataModel mainCourseDataModel = _mainCourseDomainMapper.MapMainCourseToMainCourseDataModel(mainCourse);

            return mainCourseDataModel;
        }

        public async Task AddMainCourseAsync(MainCourseDataModel mainCourseDataModel)
        {
            MainCourse mainCourse = _mainCourseDomainMapper.MapMainCourseDataModelToMainCourse(mainCourseDataModel);

            await _mainCourseRepository.AddAsync(mainCourse);
            await _mainCourseRepository.SaveChangesAsync();

        }

        public async Task EditMainCourseAsync(MainCourseDataModel mainCourseDataModel)
        {
            MainCourse mainCourse = await _mainCourseRepository.GetByIdAsync(mainCourseDataModel.Id, true) ?? throw new NotFoundException("Main Course Not Found");

            mainCourse.CuisineId = mainCourseDataModel.CuisineId;
            mainCourse.Name = mainCourseDataModel.Name;
            mainCourse.ImageData = mainCourseDataModel.ImageData;

            _mainCourseRepository.Update(mainCourse);
            await _mainCourseRepository.SaveChangesAsync();

        }

        public async Task DeleteMainCourseAsync(Guid id)
        {
            MainCourse mainCourse = await _mainCourseRepository.GetByIdAsync(id, true)
                                ?? throw new NotFoundException("Main Course Not Found");

            _mainCourseRepository.Remove(mainCourse);
            await _mainCourseRepository.SaveChangesAsync();

        }

        public async Task DeleteMainCourseByCuisineIdAsync(Guid id, bool withTracking)
        {
            IEnumerable<MainCourse> mainCourses = await _mainCourseRepository.GetAllMainCourseByCuisineIdAsync(id, withTracking);

            _mainCourseRepository.RemoveRange(mainCourses);
            await _mainCourseRepository.SaveChangesAsync();
        }
    }
}
