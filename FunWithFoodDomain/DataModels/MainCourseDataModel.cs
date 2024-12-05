namespace FunWithFoodDomain.DataModels
{
    public class MainCourseDataModel
    {
        public Guid Id { get; set; }
        public Guid CuisineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? ImageData { get; set; }
    }
}
