using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class Multimedia : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public string CoverPath { get; set; }

        public ICollection<Actor> Actors { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
