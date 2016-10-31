using System.Collections.Generic;

namespace Assignment3.Models
{
    public class VideoDTO
    {
        public int id           { get; set; }
        public string title     { get; set; }
        public string source    { get; set; }
        public string creator   { get; set; }
        public int channelId    { get; set; }
    }
}