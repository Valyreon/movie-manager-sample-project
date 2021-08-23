using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Rating : BaseEntity
    {
        [Range(1, 5)]
        public byte Value { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public int? MovieId { get; set; }

        public int? SeriesId { get; set; }
    }
}
