using FunWithFood.Dto.Food;
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
    public class FoodController : Controller
    {
        private readonly ILogger<FoodController> _logger;
        private readonly IFoodApplicationService _foodApplicationService;
        private readonly ICuisineApplicationService _cuisineApplicationService;
        private readonly IJwtTokenHandler _jwtTokenHandler;

        public FoodController(ILogger<FoodController> logger, 
                              IFoodApplicationService foodApplicationService, 
                              ICuisineApplicationService cuisineApplicationService,
                              IJwtTokenHandler jwtTokenHandler)
        {
            _logger = logger;
            _foodApplicationService = foodApplicationService;
            _cuisineApplicationService = cuisineApplicationService;
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<FoodDisplayViewModel> foodDisplayViewModels = (await _foodApplicationService.GetAllFoodDisplayViewModel()).ToList();

            if (!foodDisplayViewModels.Any())
            {
                return View(new FoodDisplayViewModel { Name = Constant.NoFoodAvailable, CuisineType = string.Empty, ImageBase64 = null });
            }

            FoodDisplayViewModel randomSelectedFood = UtilityMethods.SelectRandomFoodToDisplay(foodDisplayViewModels);

            return View(randomSelectedFood);
        }

        [HttpGet]
        public async Task<IActionResult> Foods()
        {
            List<FoodDisplayViewModel> foodDisplayViewModels = 
                            (await _foodApplicationService.GetAllFoodDisplayViewModel()).ToList();
            string? token = HttpContext.Request.Cookies["AuthToken"];
            bool isTokenValid = !string.IsNullOrEmpty(token) && _jwtTokenHandler.IsTokenValid(token);

            List<CuisineViewModel> cuisineViewModels = (await _cuisineApplicationService.GetAllCuisineViewModelAsync()).ToList();
            ViewBag.CuisineList = new SelectList(cuisineViewModels, "Type", "Type");
            ViewBag.IsTokenValid = isTokenValid;

            return View(foodDisplayViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetRandomFood()
        {
            List<FoodDisplayViewModel> foodDisplayViewModels = (await _foodApplicationService.GetAllFoodDisplayViewModel()).ToList();

            if (!foodDisplayViewModels.Any())
            {
                return Json(new FoodDisplayViewModel { Name = Constant.NoFoodAvailable, CuisineType = string.Empty, ImageBase64 = null });
            }

            FoodDisplayViewModel randomFood = foodDisplayViewModels[new Random().Next(foodDisplayViewModels.Count)];
            return Json(randomFood);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> AddFoodPage()
        {
            List<CuisineViewModel> cuisineViewModels = (await _cuisineApplicationService.GetAllCuisineViewModelAsync()).ToList();
            ViewBag.CuisineList = new SelectList(cuisineViewModels, "Id", "Type");
            return View();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> AddFood(AddFoodDto addFoodDto)
        {
            await _foodApplicationService.AddFoodAsync(addFoodDto);
            return RedirectToAction("Foods");

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> EditFood(EditFoodDto editFoodDto)
        {
            await _foodApplicationService.EditFoodAsync(editFoodDto);

            return RedirectToAction("Foods");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> EditFoodPage(Guid id)
        {
            FoodViewModel? foodViewModel = await _foodApplicationService.GetFoodViewModelByIdAsync(id);

            if (foodViewModel is null)
            {
                return RedirectToAction("Foods");
            }

            List<CuisineViewModel> cuisineViewModels = (await _cuisineApplicationService.GetAllCuisineViewModelAsync()).ToList();
            ViewBag.CuisineList = new SelectList(cuisineViewModels, "Id", "Type");

            EditFoodDto editFoodDto = new EditFoodDto
            {
                Id = foodViewModel.Id,
                CuisineId = foodViewModel.CuisineId,
                Name = foodViewModel.Name,
                ImageBase64 = foodViewModel.ImageBase64,
            };

            return View(editFoodDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteFood(Guid id)
        {
            await _foodApplicationService.DeleteFoodAsync(id);

            return RedirectToAction("Foods");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
