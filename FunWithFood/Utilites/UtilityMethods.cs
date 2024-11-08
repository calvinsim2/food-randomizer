using FunWithFood.ViewModels;
using FunWithFoodDomain.Constants;

namespace FunWithFood.Utilites
{
    public static class UtilityMethods
    {
        public static FoodDisplayViewModel SelectRandomFoodToDisplay(IEnumerable<FoodDisplayViewModel> foods)
        {
            List<FoodDisplayViewModel> foodsList = foods.ToList();

            if (foodsList.Count == 0)
            {
                return new FoodDisplayViewModel
                {
                    Name = Constant.NoFoodAvailable,
                };
            }

            Random random = new Random();
            int randomIndex = random.Next(0, foodsList.Count);
            FoodDisplayViewModel randomSelectedFood = foodsList[randomIndex];

            return randomSelectedFood;
        }
    }
}
