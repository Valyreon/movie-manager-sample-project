using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class TVShow : Multimedia
    {
        public DateTime EndDate { get; set; }
        [Required]
        public ushort NumberOfSeasons { get; set; }
    }
}
