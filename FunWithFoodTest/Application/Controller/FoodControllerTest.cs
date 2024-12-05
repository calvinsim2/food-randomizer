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
    public class FoodControllerTest
    {
        private static readonly ILogger<FoodController> _loggerMock = A.Fake<ILogger<FoodController>>();
        private static readonly IMainCourseApplicationService _mainCourseApplicationServiceMock = A.Fake<IMainCourseApplicationService>();
        private static readonly ICuisineApplicationService _cuisineApplicationServiceMock = A.Fake<ICuisineApplicationService>();
        private static readonly IJwtTokenHandler _jwtTokenHandlerMock = A.Fake<IJwtTokenHandler>();
        private static readonly ICommonUtilityMethods _commonUtilityMethodsMock = A.Fake<ICommonUtilityMethods>();
        private readonly FoodController _mainCourseController = new FoodController(_loggerMock,
                                                                            _mainCourseApplicationServiceMock,
                                                                            _cuisineApplicationServiceMock,
                                                                            _jwtTokenHandlerMock,
                                                                            _commonUtilityMethodsMock);
        private static HttpContext _httpContextMock = A.Fake<HttpContext>();

        [Fact]
        public async Task Index_Positive_DisplayIndexPageWithExistingFood_FoodDisplayViewModelShouldBeSelected()
        {
            // Arrange
            MainCourseDisplayViewModel foodOneMock = new MainCourseDisplayViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Nasi Lemak",
                CuisineType = "Muslim",
                ImageBase64 = null
            };

            List<MainCourseDisplayViewModel> foodDisplayViewModelsMock = new List<MainCourseDisplayViewModel> { foodOneMock };

            A.CallTo(() => _mainCourseApplicationServiceMock.GetAllMainCourseDisplayViewModel())
                                                      .WithAnyArguments()
                                                      .Returns(foodDisplayViewModelsMock);

            // Act
            IActionResult result = await _mainCourseController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            MainCourseDisplayViewModel model = Assert.IsAssignableFrom<MainCourseDisplayViewModel>(viewResult.ViewData.Model);
            Assert.Equal("Nasi Lemak", model.Name);
            Assert.Equal("Muslim", model.CuisineType);
        }

        [Fact]
        public async Task Index_Positive_DisplayIndexPageWithoutExistingFood_FoodDisplayViewModelShouldBeNull()
        {
            // Arrange

            List<MainCourseDisplayViewModel> foodDisplayViewModelsMock = new List<MainCourseDisplayViewModel>();

            A.CallTo(() => _mainCourseApplicationServiceMock.GetAllMainCourseDisplayViewModel())
                                                      .WithAnyArguments()
                                                      .Returns(foodDisplayViewModelsMock);

            // Act
            IActionResult result = await _mainCourseController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            MainCourseDisplayViewModel model = Assert.IsAssignableFrom<MainCourseDisplayViewModel>(viewResult.ViewData.Model);
            Assert.Equal(Constant.NoMainCourseAvailable, model.Name);
            Assert.Equal(string.Empty, model.CuisineType);
        }

        [Fact]
        public async Task Foods_Positive_FoodAndCuisineAvailable_DisplayFoodDisplayViewModelAndCuisineList()
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
            IActionResult result = await _mainCourseController.Foods();

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            var cuisineList = Assert.IsType<SelectList>(_mainCourseController.ViewBag.CuisineList);
            Assert.Equal(2, cuisineList.Items.Count);

            Assert.True((bool)_mainCourseController.ViewBag.IsTokenValid);
        }

        [Fact]
        public async Task GetRandomFood_Positive_FoodExist_ReturnRandomFood()
        {
            // Arrange
            List<MainCourseDisplayViewModel> foodDisplayViewModelsMock = new List<MainCourseDisplayViewModel>
            {
                new MainCourseDisplayViewModel { Name = "Pizza", CuisineType = "Italian" },
                new MainCourseDisplayViewModel { Name = "Sushi", CuisineType = "Japanese" }
            };

            A.CallTo(() => _mainCourseApplicationServiceMock.GetAllMainCourseDisplayViewModel()).Returns(foodDisplayViewModelsMock);

            A.CallTo(() => _commonUtilityMethodsMock.GenerateRandomInteger(foodDisplayViewModelsMock.Count))
                                                    .WithAnyArguments().Returns(0);

            // Act
            IActionResult result = await _mainCourseController.GetRandomFood();

            // Assert
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);

            Assert.NotNull(jsonResult.Value);
            MainCourseDisplayViewModel foodDisplayViewModel = (MainCourseDisplayViewModel)jsonResult.Value;

            Assert.Equal("Pizza", foodDisplayViewModel.Name);
            Assert.Equal("Italian", foodDisplayViewModel.CuisineType);
        }

        [Fact]
        public async Task GetRandomFood_Negative_NoFoodExists_ReturnDefaultValue()
        {
            // Arrange
            List<MainCourseDisplayViewModel> foodDisplayViewModelsMock = new List<MainCourseDisplayViewModel>();

            A.CallTo(() => _mainCourseApplicationServiceMock.GetAllMainCourseDisplayViewModel()).Returns(foodDisplayViewModelsMock);

            A.CallTo(() => _commonUtilityMethodsMock.GenerateRandomInteger(foodDisplayViewModelsMock.Count))
                                                    .WithAnyArguments().Returns(0);

            // Act
            IActionResult result = await _mainCourseController.GetRandomFood();

            // Assert
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);

            Assert.NotNull(jsonResult.Value);
            MainCourseDisplayViewModel foodDisplayViewModel = (MainCourseDisplayViewModel)jsonResult.Value;

            Assert.Equal(Constant.NoMainCourseAvailable, foodDisplayViewModel.Name);
            Assert.Equal(string.Empty, foodDisplayViewModel.CuisineType);
        }

        [Fact]
        public async Task AddFoodPage_Positive_ShouldReturnViewWithCuisineList()
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
            IActionResult result = await _mainCourseController.AddFoodPage();

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
        public async Task AddFood_Positive_AddFoodAsyncShouldBeCalled()
        {
            // Arrange
            AddMainCourseDto addFoodDto = new AddMainCourseDto
            {
                CuisineId = Guid.NewGuid(),
                Name = "Chinese",
                ImageFile = null,
            };

            // Act
            IActionResult result = await _mainCourseController.AddFood(addFoodDto);

            // Assert
            RedirectToActionResult viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Foods", viewResult.ActionName);
            Assert.Null(viewResult.ControllerName); // Ensure no redirection to a different controller

            A.CallTo(() => _mainCourseApplicationServiceMock.AddMainCourseAsync(addFoodDto))
                .MustHaveHappened();

        }

        [Fact]
        public async Task EditFood_Positive_EditFoodAsyncShouldBeCalled()
        {
            // Arrange
            EditMainCourseDto editFoodDtoMock = new EditMainCourseDto
            {
                Id = Guid.NewGuid(),
                CuisineId = Guid.NewGuid(),
                Name = "Mee Pok"
            };

            // Act
            IActionResult result = await _mainCourseController.EditFood(editFoodDtoMock);

            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            A.CallTo(() => _mainCourseApplicationServiceMock.EditMainCourseAsync(editFoodDtoMock))
                .MustHaveHappened();

            Assert.Equal("Foods", redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName);
        }

        [Fact]
        public async Task EditFoodPage_Positive_FoodExist_ShouldDisplayEditFoodPage()
        {
            // Arrange
            Guid mockId = Guid.NewGuid();
            MainCourseViewModel foodViewModel = new MainCourseViewModel
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
                                                      .Returns(foodViewModel);

            A.CallTo(() => _cuisineApplicationServiceMock.GetAllCuisineViewModelAsync()).Returns(mockCuisineList);

            // Act
            IActionResult result = await _mainCourseController.EditFoodPage(mockId);

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
        public async Task EditFoodPage_Negative_找不到食物_ShouldRedirectToFoods()
        {
            // Arrange
            MainCourseViewModel? foodViewModel = null;
            Guid mockId = Guid.NewGuid();

            A.CallTo(() => _mainCourseApplicationServiceMock.GetMainCourseViewModelByIdAsync(mockId))
                                                      .WithAnyArguments()
                                                      .Returns(foodViewModel);

            // Act
            IActionResult result = await _mainCourseController.EditFoodPage(mockId);


            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Foods", redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName);
        }

        [Fact]
        public async Task DeleteFood_Positive_DeleteExistingFood_ShouldCallDeleteFood()
        {
            // Arrange
            Guid mockId = Guid.NewGuid();

            // Act
            IActionResult result = await _mainCourseController.DeleteFood(mockId);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            A.CallTo(() => _mainCourseApplicationServiceMock.DeleteMainCourseAsync(mockId))
                .MustHaveHappened();
        }
    }
}
