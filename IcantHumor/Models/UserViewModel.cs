using IcantHumor.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace IcantHumor.Models
{
    public class UserViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Roles Role { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        public bool ConfirmEmail { get; set; }
        public string? ProfilePicture { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Minimum symbols - 6")]
        [RegularExpression("^(?=.*[A-Z])(?=.*\\d)[A-Za-z\\d]+$", ErrorMessage = "Only english. At least one number and capital letter")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
