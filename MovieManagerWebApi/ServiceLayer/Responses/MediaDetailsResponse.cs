using System.Collections.Generic;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer.Responses
{
    public abstract class MediaDetailsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public string CoverPath { get; set; }

        public IEnumerable<string> Actors { get; set; }
        public IEnumerable<RatingData> Ratings { get; set; }

        public double? AverageRating { get; set; }
    }
}
