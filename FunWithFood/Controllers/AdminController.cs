using FunWithFood.Dto.Admin;
using FunWithFood.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FunWithFood.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<FoodController> _logger;
        private readonly IAdminApplicationService _adminApplicationService;

        public AdminController(ILogger<FoodController> logger,
                              IAdminApplicationService adminApplicationService)
        {
            _logger = logger;
            _adminApplicationService = adminApplicationService;
        }

        [HttpGet]
        public ActionResult AddAdminPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddAdmin(AddAdminDto addAdminDto) 
        {
            await _adminApplicationService.RegisterAdminAccountAsync(addAdminDto);

            return RedirectToAction("LoginPage");
        }
        public ActionResult LoginPage()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ExistingAdminAccountExist()
        {
            bool recordExists = await _adminApplicationService.CheckIfRecordsExistInDatabaseAsync();

            return Json(recordExists);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto loginDto) 
        {
            string token = await _adminApplicationService.LoginUserAsync(loginDto);

            if (!string.IsNullOrEmpty(token))
            {

                Response.Cookies.Append("AuthToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    Expires = DateTime.UtcNow.AddDays(1) 
                });

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Invalid login" });
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return RedirectToAction("LoginPage");
        }


    }
}
