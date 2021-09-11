using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Requests
{
    public class RateRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int MovieId { get; set; }
        [Required]
        [Range(1, 5)]
        public byte Value { get; set; }
    }
}
