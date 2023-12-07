using IcantHumor.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace IcantHumor.Models
{
    public class UserViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Roles Role { get; set; }
        public string? UserName { get; set; }
        public string? ProfilePicture { get; set; }

        public FullUserInfoViewModel? FullUserInfo { get; set; }
        public List<FavouriteViewModel> Favourites { get; set; } = new();
    }
}
