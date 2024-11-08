using FunWithFoodDomain.Models.Common;

namespace FunWithFoodDomain.Models
{
    public class Food : BaseEntity
    {
        public Guid CuisineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? ImageData { get; set; }
    }
}
