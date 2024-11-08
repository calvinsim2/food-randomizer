using FunWithFood.Dto.Cuisine;
using FunWithFood.Interfaces;
using FunWithFood.ViewModels;
using FunWithFoodDomain.Interfaces.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunWithFood.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuisineController : Controller
    {
        private readonly ILogger<CuisineController> _logger;
        private readonly ICuisineApplicationService _cuisineApplicationService;
        private readonly IJwtTokenHandler _jwtTokenHandler;

        public CuisineController(ILogger<CuisineController> logger, ICuisineApplicationService cuisineApplicationService,
                                 IJwtTokenHandler jwtTokenHandler)
        {
            _logger = logger;
            _cuisineApplicationService = cuisineApplicationService;
            _jwtTokenHandler = jwtTokenHandler;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Cuisine()
        {
            List<CuisineViewModel> cuisines = (await _cuisineApplicationService.GetAllCuisineViewModelAsync()).ToList();

            string? token = HttpContext.Request.Cookies["AuthToken"];
            bool isTokenValid = !string.IsNullOrEmpty(token) && _jwtTokenHandler.IsTokenValid(token);

            ViewBag.IsTokenValid = isTokenValid;
            return View(cuisines);
        }

        [HttpGet]
        public IActionResult AddCuisinePage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCuisine(AddCuisineDto cuisineDto)
        {
            await _cuisineApplicationService.AddCuisineAsync(cuisineDto);

            return RedirectToAction("Cuisine");
        }

        [HttpGet]
        public async Task<IActionResult> EditCuisinePage(Guid id)
        {
            CuisineViewModel? cuisineViewModel = await _cuisineApplicationService.GetCuisineViewModelByIdAsync(id);

            if (cuisineViewModel is null)
            {
                return RedirectToAction("Cuisine");
            }

            EditCuisineDto editCuisineDto = new EditCuisineDto
            {
                Id = cuisineViewModel.Id,
                Type = cuisineViewModel.Type
            };

            return View(editCuisineDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditCuisine(EditCuisineDto editCuisineDto)
        {
            await _cuisineApplicationService.EditCuisineAsync(editCuisineDto);
            return RedirectToAction("Cuisine");
        }

        
        public async Task<IActionResult> DeleteCuisine(Guid id)
        {
            await _cuisineApplicationService.DeleteCuisineAsync(id);

            return RedirectToAction("Cuisine");
        }
    }
}
