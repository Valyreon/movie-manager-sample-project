using ServiceLayer.Responses.JsonModels;
using System.Collections.Generic;

namespace ServiceLayer.Responses
{
    public class MovieDetailsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public string CoverPath { get; set; }

        public IEnumerable<string> Actors { get; set; }

        public double? AverageRating { get; set; }
    }
}
