using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Requests
{
    public class ReviewRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int MovieId { get; set; }
        [Required]
        [Range(1, 10)]
        public byte Rating { get; set; }

        [StringLength(200)]
        public string Text { get; set; }
    }
}
