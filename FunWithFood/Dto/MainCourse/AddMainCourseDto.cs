namespace FunWithFood.Dto.MainCourse
{
    public class AddMainCourseDto
    {
        public Guid CuisineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }
    }
}
