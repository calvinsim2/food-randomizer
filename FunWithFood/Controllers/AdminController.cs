using FunWithFood.Dto.Admin;
using FunWithFood.Interfaces;
using FunWithFoodDomain.Common.Exceptions;
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
            try
            {
                string token = await _adminApplicationService.LoginUserAsync(loginDto);

                Response.Cookies.Append("AuthToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    Expires = DateTime.UtcNow.AddDays(1)
                });

                return Json(new { success = true });
            }
            catch (NotFoundException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex) 
            {
                return Json(new { success = false, message = "An error occured. Please try again later." });
            }
            
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return RedirectToAction("LoginPage");
        }


    }
}
