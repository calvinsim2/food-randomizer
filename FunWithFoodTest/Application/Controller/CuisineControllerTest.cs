using FakeItEasy;
using FunWithFood.Controllers;
using FunWithFood.Dto.Cuisine;
using FunWithFood.Interfaces;
using FunWithFood.ViewModels;
using FunWithFoodDomain.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace FunWithFoodTest.Application.Controller
{
    public class CuisineControllerTest
    {
        private static readonly ILogger<CuisineController> _loggerMock = A.Fake<ILogger<CuisineController>>();
        private static readonly ICuisineApplicationService _cuisineApplicationServiceMock = A.Fake<ICuisineApplicationService>();
        private static readonly IJwtTokenHandler _jwtTokenHandlerMock = A.Fake<IJwtTokenHandler>();

        private readonly CuisineController _cuisineController = new CuisineController(_loggerMock, _cuisineApplicationServiceMock,
                                                                                      _jwtTokenHandlerMock);

        private static HttpContext _httpContextMock = A.Fake<HttpContext>();

        private static readonly ITempDataProvider _tempDataProviderMock = A.Fake<ITempDataProvider>();
        private static readonly TempDataDictionary tempData = new TempDataDictionary(_httpContextMock, _tempDataProviderMock);


        private void SetupControllerRequiredData()
        {
            _cuisineController.ControllerContext = new ControllerContext { HttpContext = _httpContextMock };
            _cuisineController.TempData = tempData;
        }

        [Fact]
        public async Task Cuisine_Positive_CuisineExists_ShouldDisplayCuisinePageWithExistingCuisine()
        {
            SetupControllerRequiredData();

            CuisineViewModel cuisineViewModelMock = new CuisineViewModel
            {
                Id = Guid.NewGuid(),
                Type = "Chinese"
            };

            CuisineViewModel cuisineViewModelTwoMock = new CuisineViewModel
            {
                Id = Guid.NewGuid(),
                Type = "Muslim"
            };

            List<CuisineViewModel> cuisineViewModelsMock = new List<CuisineViewModel> { cuisineViewModelMock, cuisineViewModelTwoMock };

            A.CallTo(() => _cuisineApplicationServiceMock.GetAllCuisineViewModelAsync()).Returns(cuisineViewModelsMock);

            A.CallTo(() => _httpContextMock.Request.Cookies["AuthToken"]).Returns("valid-token");

            A.CallTo(() => _jwtTokenHandlerMock.IsTokenValid("valid-token")).Returns(true);

            IActionResult result = await _cuisineController.Cuisine();

            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            List<CuisineViewModel> cuisineViewModels = Assert.IsAssignableFrom<List<CuisineViewModel>>(viewResult.ViewData.Model);

            Assert.Equal("Chinese", cuisineViewModels[0].Type);
            Assert.Equal("Muslim", cuisineViewModels[1].Type);
            Assert.True((bool)_cuisineController.ViewBag.IsTokenValid);
        }

        [Fact]
        public void AddCuisinePage_Positive_ShouldDisplayCuisinePage()
        {
            IActionResult result = _cuisineController.AddCuisinePage();

            ViewResult viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task AddCuisine_Positive_OnAddingCuisine_ShouldRedirectToCuisinePage()
        {
            AddCuisineDto addCuisineDtoMock = new AddCuisineDto
            {
                Type = "Chinese"
            };

            IActionResult result = await _cuisineController.AddCuisine(addCuisineDtoMock);

            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Cuisine", redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName); // Ensure no redirection to a different controller

            A.CallTo(() => _cuisineApplicationServiceMock.AddCuisineAsync(addCuisineDtoMock)).MustHaveHappened();
        }

        [Fact]
        public async Task EditCuisinePage_Positive_CuisineViewModelFound_ShouldDisplayEditFoodPage()
        {
            Guid mockGuid = Guid.NewGuid();

            CuisineViewModel cuisineViewModelMock = new CuisineViewModel
            {
                Id = mockGuid,
                Type = "Italian"
            };

            EditCuisineDto editCuisineDtoMock = new EditCuisineDto
            {
                Id = cuisineViewModelMock.Id,
                Type = cuisineViewModelMock.Type,
            };

            A.CallTo(() => _cuisineApplicationServiceMock.GetCuisineViewModelByIdAsync(Guid.NewGuid()))
                .WithAnyArguments().Returns(cuisineViewModelMock);

            IActionResult result = await _cuisineController.EditCuisinePage(Guid.NewGuid());

            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            EditCuisineDto editCuisineDto = Assert.IsAssignableFrom<EditCuisineDto>(viewResult.ViewData.Model);

            Assert.Equal(mockGuid, editCuisineDto.Id);
            Assert.Equal("Italian", editCuisineDto.Type);

        }

        [Fact]
        public async Task EditCuisinePage_Negative_CuisineViewModelNotFound_ShouldRedirectToCuisine()
        {
            CuisineViewModel cuisineViewModelMock = null;

            A.CallTo(() => _cuisineApplicationServiceMock.GetCuisineViewModelByIdAsync(Guid.NewGuid()))
                .WithAnyArguments().Returns(cuisineViewModelMock);

            IActionResult result = await _cuisineController.EditCuisinePage(Guid.NewGuid());

            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Cuisine", redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName); // Ensure no redirection to a different controller
        }

        [Fact]
        public async Task EditCuisine_Positive_EditCuisineCalled_ShouldRedirectToCuisine()
        {
            Guid mockGuid = Guid.NewGuid();

            EditCuisineDto editCuisineDtoMock = new EditCuisineDto
            {
                Id = mockGuid,
                Type = "Italian",
            };

            IActionResult result = await _cuisineController.EditCuisine(editCuisineDtoMock);

            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Cuisine", redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName); // Ensure no redirection to a different controller

            A.CallTo(() => _cuisineApplicationServiceMock.EditCuisineAsync(editCuisineDtoMock)).MustHaveHappened();
        }

        [Fact]
        public async Task DeleteCuisine_Positive_DeleteCuisineCalled_ShouldRedirectToCuisine()
        {
            Guid mockGuid = Guid.NewGuid();
            IActionResult result = await _cuisineController.DeleteCuisine(mockGuid);

            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Cuisine", redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName); // Ensure no redirection to a different controller

            A.CallTo(() => _cuisineApplicationServiceMock.DeleteCuisineAsync(mockGuid)).MustHaveHappened();
        }
    }
}
