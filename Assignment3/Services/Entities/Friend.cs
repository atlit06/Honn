using System.ComponentModel.DataAnnotations;

namespace Assignment3.Services.Entities
{
    public class Friend
    {
        public int friendee    { get; set; }
        public int friended    { get; set; }
    }
}