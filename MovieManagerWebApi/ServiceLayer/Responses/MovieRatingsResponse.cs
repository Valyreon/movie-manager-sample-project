using ServiceLayer.Responses.JsonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Responses
{
    public class MovieRatingsResponse
    {
        public IEnumerable<RatingData> Ratings { get; set; }
    }
}
