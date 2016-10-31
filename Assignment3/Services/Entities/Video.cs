using System.ComponentModel.DataAnnotations;

namespace Assignment3.Services.Entities
{
    public class Video
    {
        [Key]
        public int id           { get; set; }
        public string title     { get; set; }
        public string source    { get; set; }
        public int creator      { get; set; }
        public int channelId    { get; set; }
    }
}