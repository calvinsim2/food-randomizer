using FunWithFood.ViewModels;
using FunWithFoodDomain.Constants;

namespace FunWithFood.Utilites
{
    public static class UtilityMethods
    {
        public static MainCourseDisplayViewModel SelectRandomFoodToDisplay(IEnumerable<MainCourseDisplayViewModel> foods)
        {
            List<MainCourseDisplayViewModel> foodsList = foods.ToList();

            if (foodsList.Count == 0)
            {
                return new MainCourseDisplayViewModel
                {
                    Name = Constant.NoMainCourseAvailable,
                };
            }

            Random random = new Random();
            int randomIndex = random.Next(0, foodsList.Count);
            MainCourseDisplayViewModel randomSelectedFood = foodsList[randomIndex];

            return randomSelectedFood;
        }
    }
}
