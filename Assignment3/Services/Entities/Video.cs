using System.ComponentModel.DataAnnotations;

namespace Assignment3.Services.Entities
{
    public class Video
    {
        [Key]
        public int ID           { get; set; }
        public string title     { get; set; }
        public string source    { get; set; }
        public User creator     { get; set; }
        public int ChannelID    { get; set; }
    }
}