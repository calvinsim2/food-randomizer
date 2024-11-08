namespace FunWithFood.ViewModels
{
    public class FoodDisplayViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CuisineType { get; set; } = string.Empty;
        public string? ImageBase64 { get; set; }
    }
}
