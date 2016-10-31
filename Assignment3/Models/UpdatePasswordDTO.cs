namespace Assignment3.Models
{
    public class UpdatePasswordDTO : AuthorizedUser
    {
        public string newPassword { get; set; }
    }
}