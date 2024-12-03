using FakeItEasy;
using FunWithFood.Controllers;
using FunWithFood.Dto.Admin;
using FunWithFood.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace FunWithFoodTest.Application.Controller
{
    public class AdminControllerTest
    {
        private static readonly ILogger<AdminController> _loggerMock = A.Fake<ILogger<AdminController>>();
        private static readonly IAdminApplicationService _adminApplicationServiceMock = A.Fake<IAdminApplicationService>();

        public readonly AdminController _adminController = new AdminController(_loggerMock, _adminApplicationServiceMock);

        private static HttpContext _httpContextMock = A.Fake<HttpContext>();

        private static readonly IResponseCookies _responseCookieMock = A.Fake<IResponseCookies>();
        private static readonly HttpResponse _responseMock = A.Fake<HttpResponse>();
        private static readonly IUrlHelper _urlHelperMock = A.Fake<IUrlHelper>();

        private static readonly ITempDataProvider _tempDataProviderMock = A.Fake<ITempDataProvider>();
        private static readonly TempDataDictionary tempData = new TempDataDictionary(_httpContextMock, _tempDataProviderMock);


        private void SetupControllerRequiredData()
        {
            _adminController.ControllerContext = new ControllerContext { HttpContext = _httpContextMock };
            _adminController.TempData = tempData;
            A.CallTo(() => _responseMock.Cookies).Returns(_responseCookieMock);
            A.CallTo(() => _httpContextMock.Response).Returns(_responseMock);

            _adminController.Url = _urlHelperMock;

            // Configure the UrlHelper to generate URLs as needed
            A.CallTo(() => _urlHelperMock.Action(A<UrlActionContext>.Ignored))
                .ReturnsLazily((UrlActionContext ctx) => $"/{ctx.Action}");
        }

        [Fact]
        public void AddAdminPage_Positive_AddAdminPageCalled_ShouldDisplayAddAdminPage()
        {
            IActionResult result = _adminController.AddAdminPage();

            ViewResult viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task AddAdmin_Positive_WhenSuccessfullyAddAdmin_ShouldRedirectToLoginPage()
        {
            AddAdminDto adminDto = new AddAdminDto
            {
                Password = "password",
            };

            IActionResult result = await _adminController.AddAdmin(adminDto);

            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("LoginPage", redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName); // Ensure no redirection to a different controller

            A.CallTo(() => _adminApplicationServiceMock.RegisterAdminAccountAsync(adminDto)).MustHaveHappened();
        }

        [Fact]
        public async Task ExistingAdminAccountExist_Positive_IfAdminAccountExist_ShouldReturnTrue()
        {
            // Arrange
            A.CallTo( () => _adminApplicationServiceMock.CheckIfRecordsExistInDatabaseAsync()).Returns(true);

            // Act
            IActionResult result = await _adminController.ExistingAdminAccountExist();

            // Assert
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);

            Assert.NotNull(jsonResult.Value);
            bool adminAccountExist = (bool)jsonResult.Value;
            Assert.True(adminAccountExist);
        }

        [Fact]
        public async Task ExistingAdminAccountExist_Negative_IfAdminAccountDoNotExist_ShouldReturnFalse()
        {
            // Arrange
            A.CallTo(() => _adminApplicationServiceMock.CheckIfRecordsExistInDatabaseAsync()).Returns(false);

            // Act
            IActionResult result = await _adminController.ExistingAdminAccountExist();

            // Assert
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);

            Assert.NotNull(jsonResult.Value);
            bool adminAccountExist = (bool)jsonResult.Value;
            Assert.False(adminAccountExist);
        }

        [Fact]
        public void Logout_Positive_IfLogoutIsCalled_ShouldRedirectToLoginPage()
        {
            SetupControllerRequiredData();
            IActionResult result = _adminController.Logout();

            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("LoginPage", redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName); // Ensure no redirection to a different controller

            A.CallTo(() => _responseCookieMock.Delete("AuthToken")).MustHaveHappened();
        }
    }
}
