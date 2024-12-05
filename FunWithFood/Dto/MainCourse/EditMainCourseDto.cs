namespace FunWithFood.Dto.MainCourse
{
    public class EditMainCourseDto
    {
        public Guid Id { get; set; }
        public Guid CuisineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ImageBase64 { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
