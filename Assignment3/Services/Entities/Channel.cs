using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Assignment3.Services.Entities
{
    public class Channel
    {
        [Key]
        public int ID               { get; set; }
        public string title         { get; set; }
    }
}   