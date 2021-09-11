using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTimeOffset ModifiedWhen { get; set; }
        [Required]
        public DateTimeOffset CreatedWhen { get; set; }
    }
}
