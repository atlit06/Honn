using System.ComponentModel.DataAnnotations;

namespace Assignment3.Services.Entities
{
    public class FavouriteVideo
    {
        public int userId   { get; set; }
        public int videoId  { get; set; }
    }
}