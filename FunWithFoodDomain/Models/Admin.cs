using FunWithFoodDomain.Models.Common;

namespace FunWithFoodDomain.Models
{
    public class Admin : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
