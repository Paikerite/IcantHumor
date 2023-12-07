using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IcantHumor.Models
{
    public class FavouriteViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdReactedUser { get; set; }
        public Guid IdPost { get; set; }
        [ForeignKey("IdPost")]
        public MediaViewModel? FavMedia { get; set; }
    }
}
