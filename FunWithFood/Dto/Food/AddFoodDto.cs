namespace FunWithFood.Dto.Food
{
    public class AddFoodDto
    {
        public Guid CuisineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }
    }
}
