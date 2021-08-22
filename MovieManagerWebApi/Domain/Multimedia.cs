using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        [NotMapped]
        public double? AverageRating
        {
            get
            {
                if (Ratings?.Any() != true)
                {
                    return null;
                }

                return Ratings.Select(r => Convert.ToDouble(r.Value)).Average();
            }
        }
    }
}
