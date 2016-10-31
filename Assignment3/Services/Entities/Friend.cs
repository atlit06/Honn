using System.ComponentModel.DataAnnotations;

namespace Assignment3.Services.Entities
{
    public class Friend
    {
        [Key]
        public string friendee    { get; set; }
        public string friended    { get; set; }
    }
}