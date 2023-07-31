using IcantHumor.Models.Enums;

namespace IcantHumor.Models
{
    public class SessionUserModel
    {
        public Guid Id { get; set; }
        public Roles Role { get; set; }
        public string UserName { get; set; }
    }
}
