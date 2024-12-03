namespace FunWithFood.ViewModels
{
    public class DessertViewModel
    {
        public Guid Id { get; set; }
        public Guid CuisineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ImageBase64 { get; set; }
    }
}
