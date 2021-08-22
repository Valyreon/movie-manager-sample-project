using System;
using System.Collections.Generic;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer.Responses
{
    public class MovieDetailsResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverPath { get; set; }
        public DateTime ReleaseDate { get; set; }

        public IEnumerable<string> Actors { get; set; }
        public IEnumerable<RatingData> Ratings { get; set; }

        public double? AverageRating { get; set; }
    }
}
