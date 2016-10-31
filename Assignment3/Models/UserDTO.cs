using System.Collections.Generic;

namespace Assignment3.Models
{
    public class UserDTO : PublicUserDTO
    {
        public string fullName  { get; set; }
        public string password  { get; set; }
    }
}