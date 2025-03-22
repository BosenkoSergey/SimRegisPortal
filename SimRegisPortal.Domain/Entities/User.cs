using SimRegisPortal.Domain.Entities.Common;

namespace SimRegisPortal.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
    }
}
