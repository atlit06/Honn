namespace Assignment3.Models
{
    public class UpdatePasswordDTO : AuthorizedUserDTO
    {
        public string newPassword { get; set; }
    }
}