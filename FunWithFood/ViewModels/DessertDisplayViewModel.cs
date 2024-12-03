namespace FunWithFood.ViewModels
{
    public class DessertDisplayViewModel
    {
        public Guid Id { get; set; }
        public string CuisineType { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ImageBase64 { get; set; }
    }
}
