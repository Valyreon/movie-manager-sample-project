using System.Collections.Generic;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer.Responses
{
    public class MovieListResponse
    {
        public IEnumerable<MovieListItem> Items { get; set; }
    }
}
