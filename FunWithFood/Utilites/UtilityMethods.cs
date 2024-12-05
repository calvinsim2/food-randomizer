using FunWithFood.ViewModels;
using FunWithFoodDomain.Constants;

namespace FunWithFood.Utilites
{
    public static class UtilityMethods
    {
        public static MainCourseDisplayViewModel SelectRandomMainCourseToDisplay(IEnumerable<MainCourseDisplayViewModel> mainCourses)
        {
            List<MainCourseDisplayViewModel> mainCoursesList = mainCourses.ToList();

            if (mainCoursesList.Count == 0)
            {
                return new MainCourseDisplayViewModel
                {
                    Name = Constant.NoMainCourseAvailable,
                };
            }

            Random random = new Random();
            int randomIndex = random.Next(0, mainCoursesList.Count);
            MainCourseDisplayViewModel randomSelectedMainCourse = mainCoursesList[randomIndex];

            return randomSelectedMainCourse;
        }
    }
}
