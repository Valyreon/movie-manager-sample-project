using System;

namespace Domain
{
    public class TVShow : Multimedia
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ushort NumberOfSeasons { get; set; }
    }
}
