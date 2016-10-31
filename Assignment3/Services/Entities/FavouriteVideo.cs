using System.ComponentModel.DataAnnotations;

namespace Assignment3.Services.Entities
{
    public class FavouriteVideo
    {
        [Key]
        public int id       { get; set; }
        public int userId   { get; set; }
        public int videoId  { get; set; }
    }
}