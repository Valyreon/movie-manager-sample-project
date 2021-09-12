using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Review : BaseEntity
    {
        [Range(1, 10)]
        public byte Rating { get; set; }

        [StringLength(200)]
        public string Text { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Movie")]
        public int? MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
