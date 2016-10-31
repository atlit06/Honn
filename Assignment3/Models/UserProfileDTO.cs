using System.Collections.Generic;

namespace Assignment3.Models
{
    public class UserProfileDTO : UserDTO
    {
        public List<VideoDTO> favouriteVideos  { get; set; }
        public List<UserDTO>  friends          { get; set; }
    }
}