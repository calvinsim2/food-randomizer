using FakeItEasy;
using FunWithFood.Controllers;
using FunWithFood.Dto.Food;
using FunWithFood.Interfaces;
using FunWithFood.Services;
using FunWithFood.ViewModels;
using FunWithFoodDomain.Constants;
using FunWithFoodDomain.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace FunWithFoodTest.Controller
{
    public class FoodControllerTest
    {
        private static readonly ILogger<FoodController> _loggerMock = A.Fake<ILogger<FoodController>>();
        private static readonly IFoodApplicationService _foodApplicationServiceMock = A.Fake<IFoodApplicationService>();
        private static readonly ICuisineApplicationService _cuisineApplicationServiceMock = A.Fake<ICuisineApplicationService>();
        private static readonly IJwtTokenHandler _jwtTokenHandlerMock = A.Fake<IJwtTokenHandler>();
        private static readonly ICommonUtilityMethods _commonUtilityMethodsMock = A.Fake<ICommonUtilityMethods>();
        private readonly FoodController _foodController = new FoodController(_loggerMock,
                                                                            _foodApplicationServiceMock,
                                                                            _cuisineApplicationServiceMock,
                                                                            _jwtTokenHandlerMock,
                                                                            _commonUtilityMethodsMock);
        private static HttpContext _httpContextMock = A.Fake<HttpContext>();

        [Fact]
        public async Task Index_Positive_DisplayIndexPageWithExistingFood_FoodDisplayViewModelShouldBeSelected()
        {
            // Arrange
            FoodDisplayViewModel foodOneMock = new FoodDisplayViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Nasi Lemak",
                CuisineType = "Muslim",
                ImageBase64 = null
            };

            List<FoodDisplayViewModel> foodDisplayViewModelsMock = new List<FoodDisplayViewModel> { foodOneMock };

            A.CallTo(() => _foodApplicationServiceMock.GetAllFoodDisplayViewModel())
                                                      .WithAnyArguments()
                                                      .Returns(foodDisplayViewModelsMock);

            // Act
            IActionResult result = await _foodController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            FoodDisplayViewModel model = Assert.IsAssignableFrom<FoodDisplayViewModel>(viewResult.ViewData.Model);
            Assert.Equal("Nasi Lemak", model.Name);
            Assert.Equal("Muslim", model.CuisineType);
        }

        [Fact]
        public async Task Index_Positive_DisplayIndexPageWithoutExistingFood_FoodDisplayViewModelShouldBeNull()
        {
            // Arrange

            List<FoodDisplayViewModel> foodDisplayViewModelsMock = new List<FoodDisplayViewModel>();

            A.CallTo(() => _foodApplicationServiceMock.GetAllFoodDisplayViewModel())
                                                      .WithAnyArguments()
                                                      .Returns(foodDisplayViewModelsMock);

            // Act
            IActionResult result = await _foodController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            FoodDisplayViewModel model = Assert.IsAssignableFrom<FoodDisplayViewModel>(viewResult.ViewData.Model);
            Assert.Equal(Constant.NoFoodAvailable, model.Name);
            Assert.Equal(string.Empty, model.CuisineType);
        }

        [Fact]
        public async Task Foods_Positive_FoodAndCuisineAvailable_DisplayFoodDisplayViewModelAndCuisineList()
        {
            // Arrange

            A.CallTo(() => _foodApplicationServiceMock.GetAllFoodDisplayViewModel())
                .Returns(new List<FoodDisplayViewModel>
                {
                    new FoodDisplayViewModel { Name = "Pizza", CuisineType = "Italian" },
                    new FoodDisplayViewModel { Name = "Sushi", CuisineType = "Japanese" }
                });

            A.CallTo(() => _cuisineApplicationServiceMock.GetAllCuisineViewModelAsync())
                .Returns(new List<CuisineViewModel>
                {
                    new CuisineViewModel { Type = "Italian" },
                    new CuisineViewModel { Type = "Japanese" }
                });

            A.CallTo(() => _httpContextMock.Request.Cookies["AuthToken"]).Returns("valid-token");

            A.CallTo(() => _jwtTokenHandlerMock.IsTokenValid("valid-token")).Returns(true);


            _foodController.ControllerContext = new ControllerContext { HttpContext = _httpContextMock };

            ITempDataProvider tempDataProvider = A.Fake<ITempDataProvider>();
            TempDataDictionary tempData = new TempDataDictionary(_httpContextMock, tempDataProvider);
            _foodController.TempData = tempData;

            // Act
            IActionResult result = await _foodController.Foods();

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            var cuisineList = Assert.IsType<SelectList>(_foodController.ViewBag.CuisineList);
            Assert.Equal(2, cuisineList.Items.Count);

            Assert.True((bool)_foodController.ViewBag.IsTokenValid);
        }

        [Fact]
        public async Task GetRandomFood_Positive_FoodExist_ReturnRandomFood()
        {
            // Arrange
            List<FoodDisplayViewModel> foodDisplayViewModelsMock = new List<FoodDisplayViewModel>
            {
                new FoodDisplayViewModel { Name = "Pizza", CuisineType = "Italian" },
                new FoodDisplayViewModel { Name = "Sushi", CuisineType = "Japanese" }
            };

            A.CallTo(() => _foodApplicationServiceMock.GetAllFoodDisplayViewModel()).Returns(foodDisplayViewModelsMock);

            A.CallTo(() => _commonUtilityMethodsMock.GenerateRandomInteger(foodDisplayViewModelsMock.Count))
                                                    .WithAnyArguments().Returns(0);

            // Act
            IActionResult result = await _foodController.GetRandomFood();

            // Assert
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);

            Assert.NotNull(jsonResult.Value);
            FoodDisplayViewModel foodDisplayViewModel = (FoodDisplayViewModel)jsonResult.Value;

            Assert.Equal("Pizza", foodDisplayViewModel.Name);
            Assert.Equal("Italian", foodDisplayViewModel.CuisineType);
        }

        [Fact]
        public async Task GetRandomFood_Negative_NoFoodExists_ReturnDefaultValue()
        {
            // Arrange
            List<FoodDisplayViewModel> foodDisplayViewModelsMock = new List<FoodDisplayViewModel>();

            A.CallTo(() => _foodApplicationServiceMock.GetAllFoodDisplayViewModel()).Returns(foodDisplayViewModelsMock);

            A.CallTo(() => _commonUtilityMethodsMock.GenerateRandomInteger(foodDisplayViewModelsMock.Count))
                                                    .WithAnyArguments().Returns(0);

            // Act
            IActionResult result = await _foodController.GetRandomFood();

            // Assert
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);

            Assert.NotNull(jsonResult.Value);
            FoodDisplayViewModel foodDisplayViewModel = (FoodDisplayViewModel)jsonResult.Value;

            Assert.Equal(Constant.NoFoodAvailable, foodDisplayViewModel.Name);
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
            IActionResult result = await _foodController.AddFoodPage();

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            SelectList cuisineList = Assert.IsType<SelectList>(_foodController.ViewBag.CuisineList);
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
            AddFoodDto addFoodDto = new AddFoodDto
            {
                CuisineId = Guid.NewGuid(),
                Name = "Chinese",
                ImageFile = null,
            };

            // Act
            IActionResult result = await _foodController.AddFood(addFoodDto);

            // Assert
            RedirectToActionResult viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Foods", viewResult.ActionName);
            Assert.Null(viewResult.ControllerName); // Ensure no redirection to a different controller

            A.CallTo(() => _foodApplicationServiceMock.AddFoodAsync(addFoodDto))
                .MustHaveHappened();

        }

        [Fact]
        public async Task EditFood_Positive_EditFoodAsyncShouldBeCalled()
        {
            // Arrange
            EditFoodDto editFoodDtoMock = new EditFoodDto
            {
                Id = Guid.NewGuid(),
                CuisineId = Guid.NewGuid(),
                Name = "Mee Pok"
            };

            // Act
            IActionResult result = await _foodController.EditFood(editFoodDtoMock);

            // Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

            A.CallTo(() => _foodApplicationServiceMock.EditFoodAsync(editFoodDtoMock))
                .MustHaveHappened();

            Assert.Equal("Foods", redirectResult.ActionName);
            Assert.Null(redirectResult.ControllerName);
        }

        [Fact]
        public async Task EditFoodPage_Positive_FoodExist_ShouldDisplayEditFoodPage()
        {
            // Arrange
            Guid mockId = Guid.NewGuid();
            FoodViewModel foodViewModel = new FoodViewModel 
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

            A.CallTo(() => _foodApplicationServiceMock.GetFoodViewModelByIdAsync(mockId))
                                                      .WithAnyArguments()
                                                      .Returns(foodViewModel);

            A.CallTo(() => _cuisineApplicationServiceMock.GetAllCuisineViewModelAsync()).Returns(mockCuisineList);

            // Act
            IActionResult result = await _foodController.EditFoodPage(mockId);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            EditFoodDto model = Assert.IsAssignableFrom<EditFoodDto>(viewResult.ViewData.Model);
            Assert.Equal(mockId, model.Id);
            Assert.Equal("Mee Pok", model.Name);
            Assert.Equal(mockId, model.CuisineId);

            SelectList cuisineList = Assert.IsType<SelectList>(_foodController.ViewBag.CuisineList);
            Assert.Equal(2, cuisineList.Count());
            Assert.Contains(cuisineList, c => c.Text == "Italian");
            Assert.Contains(cuisineList, c => c.Text == "Japanese");
        }

        [Fact]
        public async Task EditFoodPage_Negative_找不到食物_ShouldRedirectToFoods()
        {
            // Arrange
            FoodViewModel? foodViewModel = null;
            Guid mockId = Guid.NewGuid();

            A.CallTo(() => _foodApplicationServiceMock.GetFoodViewModelByIdAsync(mockId))
                                                      .WithAnyArguments()
                                                      .Returns(foodViewModel);

            // Act
            IActionResult result = await _foodController.EditFoodPage(mockId);


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
            IActionResult result = await _foodController.DeleteFood(mockId);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            A.CallTo(() => _foodApplicationServiceMock.DeleteFoodAsync(mockId))
                .MustHaveHappened();
        }
    }
}
