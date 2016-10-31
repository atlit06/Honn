using System.ComponentModel.DataAnnotations;

namespace Assignment3.Services.Entities
{
    public class ChannelUser
    {
        [Key]
        public int channelID    { get; set; }
        public string userID    { get; set; }
    }
}