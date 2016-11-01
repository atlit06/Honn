using System.Collections.Generic;
namespace Assignment3.Models
{
    public class ChannelVideosDTO
    {
        public int channelID { get; set; }
        public string title { get; set; }
        public List<VideoDTO> videos { get; set; }
    }
}