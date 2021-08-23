using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Requests
{
    public class SignupRequest
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_]{8,25}$", ErrorMessage = "Username can consist only of alphanumeric characters and '_'.")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string About { get; set; }
        public bool IsPrivate { get; set; }
    }
}
