using System.ComponentModel.DataAnnotations;

namespace Assignment3.Services.Entities
{
    public class Role
    {
        [Key]
        public int roleID { get; set; }
        public string roleName { get; set; }
    }
}