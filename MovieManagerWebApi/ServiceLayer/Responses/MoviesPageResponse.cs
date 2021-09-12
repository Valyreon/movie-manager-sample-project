using System.Collections.Generic;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer.Responses
{
    public class MoviesPageResponse
    {
        public IEnumerable<MovieListItem> Items { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
