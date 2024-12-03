using FunWithFood.Dto.Dessert;
using FunWithFood.Dto.Food;
using FunWithFood.Interfaces;
using FunWithFood.ViewModels;
using FunWithFoodDomain.Interfaces.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FunWithFood.Controllers
{
    public class DessertController : Controller
    {
        private readonly ILogger<DessertController> _logger;
        private readonly IDessertApplicationService _dessertApplicationService;
        private readonly ICuisineApplicationService _cuisineApplicationService;
        private readonly IJwtTokenHandler _jwtTokenHandler;

        public DessertController(ILogger<DessertController> logger,
                              IDessertApplicationService dessertApplicationService,
                              ICuisineApplicationService cuisineApplicationService,
                              IJwtTokenHandler jwtTokenHandler)
        {
            _logger = logger;
            _dessertApplicationService = dessertApplicationService;
            _cuisineApplicationService = cuisineApplicationService;
            _jwtTokenHandler = jwtTokenHandler;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Desserts()
        {
            List<DessertDisplayViewModel> dessertDisplayViewModels =
                            (await _dessertApplicationService.GetAllDessertDisplayViewModelAsync()).ToList();
            string? token = HttpContext.Request.Cookies["AuthToken"];
            bool isTokenValid = !string.IsNullOrEmpty(token) && _jwtTokenHandler.IsTokenValid(token);

            List<CuisineViewModel> cuisineViewModels = (await _cuisineApplicationService.GetAllCuisineViewModelAsync()).ToList();
            ViewBag.CuisineList = new SelectList(cuisineViewModels, "Type", "Type");
            ViewBag.IsTokenValid = isTokenValid;

            return View(dessertDisplayViewModels);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> AddDessertPage()
        {
            List<CuisineViewModel> cuisineViewModels = (await _cuisineApplicationService.GetAllCuisineViewModelAsync()).ToList();
            ViewBag.CuisineList = new SelectList(cuisineViewModels, "Id", "Type");
            return View();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> AddDessert(AddDessertDto addDessertDto)
        {
            await _dessertApplicationService.AddDessertAsync(addDessertDto);
            return RedirectToAction("Desserts");

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> EditDessert(EditDessertDto editDessertDto)
        {
            await _dessertApplicationService.EditDessertAsync(editDessertDto);

            return RedirectToAction("Desserts");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> EditDessertPage(Guid id)
        {
            DessertViewModel? dessertViewModel = await _dessertApplicationService.GetDessertViewModelByIdAsync(id);

            if (dessertViewModel is null)
            {
                return RedirectToAction("Desserts");
            }

            List<CuisineViewModel> cuisineViewModels = (await _cuisineApplicationService.GetAllCuisineViewModelAsync()).ToList();
            ViewBag.CuisineList = new SelectList(cuisineViewModels, "Id", "Type");

            EditDessertDto editDessertDto = new EditDessertDto
            {
                Id = dessertViewModel.Id,
                CuisineId = dessertViewModel.CuisineId,
                Name = dessertViewModel.Name,
                ImageBase64 = dessertViewModel.ImageBase64,
            };

            return View(editDessertDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteDessert(Guid id)
        {
            await _dessertApplicationService.DeleteDessertAsync(id);

            return RedirectToAction("Desserts");
        }
    }
}
