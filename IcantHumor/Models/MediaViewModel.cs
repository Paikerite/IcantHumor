using IcantHumor.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IcantHumor.Models
{
    public class MediaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdOfCreator { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string UrlToFile { get; set; }
        public int Like
        {
            get
            {
                return WhoReacted.Where(w => w.ChosenReact == React.Like).Count();
            }
        }
        public int Dislike 
        {
            get
            {
                return WhoReacted.Where(w => w.ChosenReact == React.Dislike).Count();
            }
        }
        public TypeOfFile TypeOfFile { get; set; }
        public DateTime DateUpload { get; set; }
        public List<CategoryViewModel> Categories { get; set; } = new();
        public List<ReactedUserViewModel> WhoReacted { get; set; } = new();
        //public List<FavouriteViewModel> UsersFavourites { get; set; } = new();
    }
}
