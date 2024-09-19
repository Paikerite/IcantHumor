using IcantHumor.Models.Enums;

namespace IcantHumor.Models
{
    public class SessionUserModel
    {
        public Guid Id { get; set; }
        public Roles Role { get; set; }
        public required string UserName { get; set; }
    }
}
