using System.ComponentModel.DataAnnotations;

namespace Assignment3.Services.Entities
{
    public class User
    {
        [Key]
        public int userID { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
    }
}