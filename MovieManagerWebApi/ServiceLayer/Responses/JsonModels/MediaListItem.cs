using System;

namespace ServiceLayer.Responses.JsonModels
{
    public class MediaListItem
    {
        public int Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Title { get; set; }
        public string CoverPath { get; set; }
        public double? AverageRating { get; set; }
    }
}
