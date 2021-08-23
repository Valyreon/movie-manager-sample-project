using System.Collections.Generic;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer.Responses
{
    public class MoviesPageResponse : MediaPageResponse<MovieListItem>
    {
        public override IEnumerable<MovieListItem> Items { get; set; }
    }
}
