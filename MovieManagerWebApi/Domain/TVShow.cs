using System;

namespace Domain
{
    public class TVShow : Multimedia
    {
        public DateTime EndDate { get; set; }
        public ushort NumberOfSeasons { get; set; }
    }
}
