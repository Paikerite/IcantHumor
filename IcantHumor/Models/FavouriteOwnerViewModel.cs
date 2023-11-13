using System.ComponentModel.DataAnnotations;

namespace IcantHumor.Models
{
    public class FavouriteOwnerViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdOwner { get; set; }
        public List<FavouriteViewModel> Favourites { get; set; } = new();
    }
}
