using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain
{
    public class Movie : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public string CoverPath { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }

        public ICollection<Actor> Actors { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public double? CalculateAverageReview()
        {
            if (Reviews?.Any() != true)
            {
                return null;
            }

            return Reviews.Select(r => Convert.ToDouble(r.Rating)).Average();
        }
    }
}
