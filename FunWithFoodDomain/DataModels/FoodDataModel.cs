namespace FunWithFoodDomain.DataModels
{
    public class FoodDataModel
    {
        public Guid Id { get; set; }
        public Guid CuisineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? ImageData { get; set; }
    }
}
