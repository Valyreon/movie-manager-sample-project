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
        [MinLength(6)]
        [MaxLength(25)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
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
        public bool IsPrivate { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public bool IsPasswordValid(string password)
        {
            var passBytes = Encoding.UTF8.GetBytes(password);

            using var pbkdf2Hasher = new Rfc2898DeriveBytes(passBytes, Salt, 80_000, HashAlgorithmName.SHA256);
            var passHash = pbkdf2Hasher.GetBytes(256 / 8);

            return passHash.SequenceEqual(PassHash);
        }

        public void SetPassword(string password)
        {
            var passBytes = Encoding.UTF8.GetBytes(password);
            Salt = new byte[32];
            new Random().NextBytes(Salt);

            using var pbkdf2Hasher = new Rfc2898DeriveBytes(passBytes, Salt, 80_000, HashAlgorithmName.SHA256);
            PassHash = pbkdf2Hasher.GetBytes(256 / 8);
        }
    }
}
