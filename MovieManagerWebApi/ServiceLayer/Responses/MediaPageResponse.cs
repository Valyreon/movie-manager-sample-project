using System.Collections.Generic;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer.Responses
{
    public class MediaPageResponse
    {
        public IEnumerable<MediaListItem> Items { get; set; }
        public int PageNumber { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int PageSize { get; set; }
    }
}
