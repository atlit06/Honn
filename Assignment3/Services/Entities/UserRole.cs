using System.ComponentModel.DataAnnotations;

namespace Assignment3.Services.Entities
{
    public class UserRole
    {
        [Key]
        public string userID { get; set; }
        public int roleID { get; set; }
    }
}