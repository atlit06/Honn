using System.Collections.Generic;

namespace Assignment3.Models
{
    public class ProfileDTO : PublicUserDTO
    {
        public List<VideoDTO> favourites    { get; set; }
        public List<PublicUserDTO> friends  { get; set; }

    }
}