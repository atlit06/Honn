
namespace Assignment3.Models
{
    public class AuthorizedUserDTO : UserDTO
    {
        public string accessToken { get; set; }
        public string role        { get; set; }
    }
}