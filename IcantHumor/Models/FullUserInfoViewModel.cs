using System.ComponentModel.DataAnnotations;

namespace IcantHumor.Models
{
    public class FullUserInfoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string? UserEmail { get; set; }
        public bool ConfirmEmail { get; set; }
        public string? Password { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
