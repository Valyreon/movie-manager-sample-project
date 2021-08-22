using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Requests
{
    public class SignupRequest
    {
        [Required]
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
