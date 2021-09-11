using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Requests
{
    public class SignupRequest
    {
        [Required]
        [MaxLength(25)]
        [MinLength(8)]
        [RegularExpression("^[a-zA-Z0-9_]+$", ErrorMessage = "Username can consist only of alphanumeric characters and '_'.")]
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
