using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Genre : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}