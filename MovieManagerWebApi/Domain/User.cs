using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Domain
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(32)]
        [MinLength(32)]
        public byte[] Salt { get; set; }

        [Required]
        [MaxLength(32)]
        [MinLength(32)]
        public byte[] PassHash { get; set; }

        [StringLength(200)]
        public string About { get; set; }

        [Required]
        public bool Private { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public bool IsPasswordValid(string password)
        {
            using var hasher = SHA256.Create();
            var passBytes = Encoding.UTF8.GetBytes(password);
            var currentHash = hasher.ComputeHash(Salt.Concat(passBytes).ToArray());

            return currentHash.SequenceEqual(PassHash);
        }

        public void SetPassword(string password)
        {
            using var hasher = SHA256.Create();
            var passBytes = Encoding.UTF8.GetBytes(password);
            Salt = new byte[32];
            new Random().NextBytes(Salt);

            PassHash = hasher.ComputeHash(Salt.Concat(passBytes).ToArray());
        }
    }
}
