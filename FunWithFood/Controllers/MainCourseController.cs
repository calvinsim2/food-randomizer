using FunWithFood.Dto.MainCourse;
using FunWithFood.Interfaces;
using FunWithFood.Models;
using FunWithFood.Utilites;
using FunWithFood.ViewModels;
using FunWithFoodDomain.Constants;
using FunWithFoodDomain.Interfaces.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FunWithFood.Controllers
{
    public class MainCourseController : Controller
    {
        private readonly ILogger<MainCourseController> _logger;
        private readonly IMainCourseApplicationService _mainCourseApplicationService;
        private readonly ICuisineApplicationService _cuisineApplicationService;
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly ICommonUtilityMethods _commonUtilityMethods;

        public MainCourseController(ILogger<MainCourseController> logger, 
                              IMainCourseApplicationService mainCourseApplicationService, 
                              ICuisineApplicationService cuisineApplicationService,
                              IJwtTokenHandler jwtTokenHandler,
                              ICommonUtilityMethods commonUtilityMethods)
        {
            _logger = logger;
            _mainCourseApplicationService = mainCourseApplicationService;
            _cuisineApplicationService = cuisineApplicationService;
            _jwtTokenHandler = jwtTokenHandler;
            _commonUtilityMethods = commonUtilityMethods;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MainCourseDisplayViewModel> mainCourseDisplayViewModels = (await _mainCourseApplicationService.GetAllMainCourseDisplayViewModel()).ToList();

            if (!mainCourseDisplayViewModels.Any())
            {
                return View(new MainCourseDisplayViewModel { Name = Constant.NoMainCourseAvailable, CuisineType = string.Empty, ImageBase64 = null });
            }

            MainCourseDisplayViewModel randomSelectedMainCourse = UtilityMethods.SelectRandomMainCourseToDisplay(mainCourseDisplayViewModels);

            return View(randomSelectedMainCourse);
        }

        [HttpGet]
        public async Task<IActionResult> MainCourses()
        {
            List<MainCourseDisplayViewModel> mainCourseDisplayViewModels = 
                            (await _mainCourseApplicationService.GetAllMainCourseDisplayViewModel()).ToList();
            string? token = HttpContext.Request.Cookies["AuthToken"];
            bool isTokenValid = !string.IsNullOrEmpty(token) && _jwtTokenHandler.IsTokenValid(token);

            List<CuisineViewModel> cuisineViewModels = (await _cuisineApplicationService.GetAllCuisineViewModelAsync()).ToList();
            ViewBag.CuisineList = new SelectList(cuisineViewModels, "Type", "Type");
            ViewBag.IsTokenValid = isTokenValid;

            return View(mainCourseDisplayViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetRandomMainCourse()
        {
            List<MainCourseDisplayViewModel> mainCourseDisplayViewModels = (await _mainCourseApplicationService.GetAllMainCourseDisplayViewModel()).ToList();

            if (!mainCourseDisplayViewModels.Any())
            {
                return Json(new MainCourseDisplayViewModel { Name = Constant.NoMainCourseAvailable, CuisineType = string.Empty, ImageBase64 = null });
            }

            MainCourseDisplayViewModel randomMainCourse = 
                mainCourseDisplayViewModels[_commonUtilityMethods.GenerateRandomInteger(mainCourseDisplayViewModels.Count)];
            return Json(randomMainCourse);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> AddMainCoursePage()
        {
            List<CuisineViewModel> cuisineViewModels = (await _cuisineApplicationService.GetAllCuisineViewModelAsync()).ToList();
            ViewBag.CuisineList = new SelectList(cuisineViewModels, "Id", "Type");
            return View();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> AddMainCourse(AddMainCourseDto addMainCourseDto)
        {
            await _mainCourseApplicationService.AddMainCourseAsync(addMainCourseDto);
            return RedirectToAction("MainCourses");

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> EditMainCourse(EditMainCourseDto editFoodDto)
        {
            await _mainCourseApplicationService.EditMainCourseAsync(editFoodDto);

            return RedirectToAction("MainCourses");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> EditMainCoursePage(Guid id)
        {
            MainCourseViewModel? mainCourseViewModel = await _mainCourseApplicationService.GetMainCourseViewModelByIdAsync(id);

            if (mainCourseViewModel is null)
            {
                return RedirectToAction("MainCourses");
            }

            List<CuisineViewModel> cuisineViewModels = (await _cuisineApplicationService.GetAllCuisineViewModelAsync()).ToList();
            ViewBag.CuisineList = new SelectList(cuisineViewModels, "Id", "Type");

            EditMainCourseDto editMainCourseDto = new EditMainCourseDto
            {
                Id = mainCourseViewModel.Id,
                CuisineId = mainCourseViewModel.CuisineId,
                Name = mainCourseViewModel.Name,
                ImageBase64 = mainCourseViewModel.ImageBase64,
            };

            return View(editMainCourseDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteMainCourse(Guid id)
        {
            await _mainCourseApplicationService.DeleteMainCourseAsync(id);

            return RedirectToAction("MainCourses");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
