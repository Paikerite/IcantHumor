using IcantHumor.Models.Enums;
using System.ComponentModel.DataAnnotations;

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
        public int Like { get; set; }
        public int Dislike { get; set; }
        public TypeOfFile TypeOfFile { get; set; }
        public DateTime DateUpload { get; set; }

        public List<CategoryViewModel> Categories { get; set; } = new();

        //Put like or dislike
        public List<ReactedUserViewModel> WhoReacted { get; set; } = new();
    }
}
