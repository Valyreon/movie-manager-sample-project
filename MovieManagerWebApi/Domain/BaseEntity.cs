using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime ModifiedWhen { get; set; }
        [Required]
        public DateTime CreatedWhen { get; set; }
    }
}
