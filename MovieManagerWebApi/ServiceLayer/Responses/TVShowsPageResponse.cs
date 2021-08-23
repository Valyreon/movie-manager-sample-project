using System.Collections.Generic;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer.Responses
{
    public class TVShowsPageResponse : MediaPageResponse<TVShowListItem>
    {
        public override IEnumerable<TVShowListItem> Items { get; set; }
    }
}
