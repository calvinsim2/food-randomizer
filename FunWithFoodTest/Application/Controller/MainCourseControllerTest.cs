using FakeItEasy;
using FunWithFood.Controllers;
using FunWithFood.Dto.MainCourse;
using FunWithFood.Interfaces;
using FunWithFood.ViewModels;
using FunWithFoodDomain.Constants;
using FunWithFoodDomain.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace FunWithFoodTest.Application.Controller
{
    public class MainCourseControllerTest
    {
        private static readonly ILogger<MainCourseController> _loggerMock = A.Fake<ILogger<MainCourseController>>();
        private static readonly IMainCourseApplicationService _mainCourseApplicationServiceMock = A.Fake<IMainCourseApplicationService>();
        private static readonly ICuisineApplicationService _cuisineApplicationServiceMock = A.Fake<ICuisineApplicationService>();
        private static readonly IJwtTokenHandler _jwtTokenHandlerMock = A.Fake<IJwtTokenHandler>();
        private static readonly ICommonUtilityMethods _commonUtilityMethodsMock = A.Fake<ICommonUtilityMethods>();
        private readonly MainCourseController _mainCourseController = new MainCourseController(_loggerMock,
                                                                            _mainCourseApplicationServiceMock,
                                                                            _cuisineApplicationServiceMock,
                                                                            _jwtTokenHandlerMock,
                                                                            _commonUtilityMethodsMock);
        private static HttpContext _httpContextMock = A.Fake<HttpContext>();

        [Fact]
        public async Task Index_Positive_DisplayIndexPageWithExistingMainCourse_MainCourseDisplayViewModelShouldBeSelected()
        {
            // Arrange
            MainCourseDisplayViewModel MainCourseOneMock = new MainCourseDisplayViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Nasi Lemak",
                CuisineType = "Muslim",
                ImageBase64 = null
            };

            List<MainCourseDisplayViewModel> MainCourseDisplayViewModelsMock = new List<MainCourseDisplayViewModel> { MainCourseOneMock };

            A.CallTo(() => _mainCourseApplicationServiceMock.GetAllMainCourseDisplayViewModel())
                                                      .WithAnyArguments()
                                                      .Returns(MainCourseDisplayViewModelsMock);

            // Act
            IActionResult result = await _mainCourseController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            MainCourseDisplayViewModel model = Assert.IsAssignableFrom<MainCourseDisplayViewModel>(viewResult.ViewData.Model);
            Assert.Equal("Nasi Lemak", model.Name);
            Assert.Equal("Muslim", model.CuisineType);
        }

        [Fact]
        public async Task Index_Positive_DisplayIndexPageWithoutExistingMainCourse_MainCourseDisplayViewModelShouldBeNull()
        {
            // Arrange

            List<MainCourseDisplayViewModel> MainCourseDisplayViewModelsMock = new List<MainCourseDisplayViewModel>();

            A.CallTo(() => _mainCourseApplicationServiceMock.GetAllMainCourseDisplayViewModel())
                                                      .WithAnyArguments()
                                                      .Returns(MainCourseDisplayViewModelsMock);

            // Act
            IActionResult result = await _mainCourseController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            MainCourseDisplayViewModel model = Assert.IsAssignableFrom<MainCourseDisplayViewModel>(viewResult.ViewData.Model);
            Assert.Equal(Constant.NoMainCourseAvailable, model.Name);
            Assert.Equal(string.Empty, model.CuisineType);
        }

        [Fact]
        public async Task MainCourses_Positive_MainCourseAndCuisineAvailable_DisplayMainCourseDisplayViewModelAndCuisineList()
        {
            // Arrange

            A.CallTo(() => _mainCourseApplicationServiceMock.GetAllMainCourseDisplayViewModel())
                .Returns(new List<MainCourseDisplayViewModel>
                {
            new MainCourseDisplayViewModel { Name = "Pizza", CuisineType = "Italian" },
            new MainCourseDisplayViewModel { Name = "Sushi", CuisineType = "Japanese" }
                });

            A.CallTo(() => _cuisineApplicationServiceMock.GetAllCuisineViewModelAsync())
                .Returns(new List<CuisineViewModel>
                {
            new CuisineViewModel { Type = "Italian" },
            new CuisineViewModel { Type = "Japanese" }
                });

            A.CallTo(() => _httpContextMock.Request.Cookies["AuthToken"]).Returns("valid-token");

            A.CallTo(() => _jwtTokenHandlerMock.IsTokenValid("valid-token")).Returns(true);


            _mainCourseController.ControllerContext = new ControllerContext { HttpContext = _httpContextMock };

            ITempDataProvider tempDataProvider = A.Fake<ITempDataProvider>();
            TempDataDictionary tempData = new TempDataDictionary(_httpContextMock, tempDataProvider);
            _mainCourseController.TempData = tempData;

            // Act
            IActionResult result = await _mainCourseController.MainCourses();

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            var cuisineList = Assert.IsType<SelectList>(_mainCourseController.ViewBag.CuisineList);
            Assert.Equal(2, cuisineList.Items.Count);

            Assert.True((bool)_mainCourseController.ViewBag.IsTokenValid);
        }

        [Fact]
        public async Task GetRandomMainCourse_Positive_MainCourseExist_ReturnRandomMainCourse()
        {
            // Arrange
            List<MainCourseDisplayViewModel> MainCourseDisplayViewModelsMock = new List<MainCourseDisplayViewModel>
    {
        new MainCourseDisplayViewModel { Name = "Pizza", CuisineType = "Italian" },
        new MainCourseDisplayViewModel { Name = "Sushi", CuisineType = "Japanese" }
    };

            A.CallTo(() => _mainCourseApplicationServiceMock.GetAllMainCourseDisplayViewModel()).Returns(MainCourseDisplayViewModelsMock);

            A.CallTo(() => _commonUtilityMethodsMock.GenerateRandomInteger(MainCourseDisplayViewModelsMock.Count))
                                                    .WithAnyArguments().Returns(0);

            // Act
            IActionResult result = await _mainCourseController.GetRandomMainCourse();

            // Assert
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);

            Assert.NotNull(jsonResult.Value);
            MainCourseDisplayViewModel MainCourseDisplayViewModel = (MainCourseDisplayViewModel)jsonResult.Value;

            Assert.Equal("Pizza", MainCourseDisplayViewModel.Name);
            Assert.Equal("Italian", MainCourseDisplayViewModel.CuisineType);
        }

        [Fact]
        public async Task GetRandomMainCourse_Negative_NoMainCourseExists_ReturnDefaultValue()
        {
            // Arrange
            List<MainCourseDisplayViewModel> MainCourseDisplayViewModelsMock = new List<MainCourseDisplayViewModel>();

            A.CallTo(() => _mainCourseApplicationServiceMock.GetAllMainCourseDisplayViewModel()).Returns(MainCourseDisplayViewModelsMock);

            A.CallTo(() => _commonUtilityMethodsMock.GenerateRandomInteger(MainCourseDisplayViewModelsMock.Count))
                                                    .WithAnyArguments().Returns(0);

            // Act
            IActionResult result = await _mainCourseController.GetRandomMainCourse();

            // Assert
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);

            Assert.NotNull(jsonResult.Value);
            MainCourseDisplayViewModel MainCourseDisplayViewModel = (MainCourseDisplayViewModel)jsonResult.Value;

            Assert.Equal(Constant.NoMainCourseAvailable, MainCourseDisplayViewModel.Name);
            Assert.Equal(string.Empty, MainCourseDisplayViewModel.CuisineType);
        }

        [Fact]
        public async Task AddMainCoursePage_Positive_ShouldReturnViewWithCuisineList()
        {
            // Arrange
            List<CuisineViewModel> mockCuisineList = new List<CuisineViewModel>
    {
        new CuisineViewModel { Id = Guid.NewGuid(), Type = "Italian" },
        new CuisineViewModel { Id = Guid.NewGuid(), Type = "Japanese" }
    };

            A.CallTo(() => _cuisineApplicationServiceMock.GetAllCuisineViewModelAsync())
                .Returns(mockCuisineList);

            // Act
            IActionResult result = await _mainCourseController.AddMainCoursePage();

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            SelectList cuisineList = Assert.IsType<SelectList>(_mainCourseController.ViewBag.CuisineList);
            Assert.Equal(2, cuisineList.Count());
            Assert.Contains(cuisineList, c => c.Text == "Italian");
            Assert.Contains(cuisineList, c => c.Text == "Japanese");

            A.CallTo(() => _cuisineApplicationServiceMock.GetAllCuisineViewModelAsync())
                .MustHaveHappened();
        }

        [Fact]
        public async Task AddMainCourse_Positive_AddMainCourseAsyncShouldBeCalled()
        {
            // Arrange
            AddMainCourseDto addMainCourseDto = new AddMainCourseDto
            {
                CuisineId = Guid.NewGuid(),
                Name = "Chinese",
                ImageFile = null,
            };

            // Act
            IActionResult result = await _mainCourseController.AddMainCourse(addMainCourseDto);

            // Assert
            RedirectToActionResult viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MainCourses", viewResult.ActionName);
            Assert.Null(viewResult.ControllerName); // Ensure no redirection to a different controller

            A.CallTo(() => _mainCourseApplicationServiceMock.AddMainCourseAsync(addMainCourseDto))
                .MustHaveHappened();

        }

        [Fact]
        public async Task EditMainCourse_Positive_EditMainCourseAsyncShouldBeCalled()
        {
            // Arrange
            EditMainCourseDto editMainCourseDtoMock = new EditMainCourseDto
            {
                Id = Guid.NewGuid(),
                CuisineId = Guid.NewGuid(),
                Name = "Mee Pok"
            };

            // Act
            IActionResult result = await _mainCourseController.EditMainCourse(editMainCourseDtoMock);

            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            A.CallTo(() => _mainCourseApplicationServiceMock.EditMainCourseAsync(editMainCourseDtoMock))
                .MustHaveHappened();

            Assert.Equal("MainCourses", redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName);
        }

        [Fact]
        public async Task EditMainCoursePage_Positive_MainCourseExist_ShouldDisplayEditMainCoursePage()
        {
            // Arrange
            Guid mockId = Guid.NewGuid();
            MainCourseViewModel MainCourseViewModel = new MainCourseViewModel
            {
                Id = mockId,
                CuisineId = mockId,
                Name = "Mee Pok",
                ImageBase64 = null
            };

            List<CuisineViewModel> mockCuisineList = new List<CuisineViewModel>
    {
        new CuisineViewModel { Id = Guid.NewGuid(), Type = "Italian" },
        new CuisineViewModel { Id = Guid.NewGuid(), Type = "Japanese" }
    };

            A.CallTo(() => _mainCourseApplicationServiceMock.GetMainCourseViewModelByIdAsync(mockId))
                                                      .WithAnyArguments()
                                                      .Returns(MainCourseViewModel);

            A.CallTo(() => _cuisineApplicationServiceMock.GetAllCuisineViewModelAsync()).Returns(mockCuisineList);

            // Act
            IActionResult result = await _mainCourseController.EditMainCoursePage(mockId);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            EditMainCourseDto model = Assert.IsAssignableFrom<EditMainCourseDto>(viewResult.ViewData.Model);
            Assert.Equal(mockId, model.Id);
            Assert.Equal("Mee Pok", model.Name);
            Assert.Equal(mockId, model.CuisineId);

            SelectList cuisineList = Assert.IsType<SelectList>(_mainCourseController.ViewBag.CuisineList);
            Assert.Equal(2, cuisineList.Count());
            Assert.Contains(cuisineList, c => c.Text == "Italian");
            Assert.Contains(cuisineList, c => c.Text == "Japanese");
        }

        [Fact]
        public async Task EditMainCoursePage_Negative_找不到食物_ShouldRedirectToMainCourses()
        {
            // Arrange
            MainCourseViewModel? MainCourseViewModel = null;
            Guid mockId = Guid.NewGuid();

            A.CallTo(() => _mainCourseApplicationServiceMock.GetMainCourseViewModelByIdAsync(mockId))
                                                      .WithAnyArguments()
                                                      .Returns(MainCourseViewModel);

            // Act
            IActionResult result = await _mainCourseController.EditMainCoursePage(mockId);


            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MainCourses", redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName);
        }

        [Fact]
        public async Task DeleteMainCourse_Positive_DeleteExistingMainCourse_ShouldCallDeleteMainCourse()
        {
            // Arrange
            Guid mockId = Guid.NewGuid();

            // Act
            IActionResult result = await _mainCourseController.DeleteMainCourse(mockId);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            A.CallTo(() => _mainCourseApplicationServiceMock.DeleteMainCourseAsync(mockId))
                .MustHaveHappened();
        }
    }
}
