using FunWithFood.Interfaces;
using FunWithFood.ViewModels;
using FunWithFoodDomain.Interfaces.Common;
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
    }
}
