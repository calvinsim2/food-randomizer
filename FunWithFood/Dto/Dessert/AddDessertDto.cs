namespace FunWithFood.Dto.Dessert
{
    public class AddDessertDto
    {
        public Guid CuisineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }
    }
}
