
namespace Assignment3.Models
{
    public class AuthorizedUserDTO : User
    {
        public string accessToken { get; set; }
        public int roleID        { get; set; }
    }
}