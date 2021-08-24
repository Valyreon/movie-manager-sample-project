using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Requests
{
    public class SearchMediaRequest
    {
        [RegularExpression("^[\\ a-zA-Z0-9]{2,}$")]
        public string Token { get; set; }
        [Range(0, int.MaxValue)]
        public int PageNumber { get; set; } = 0;
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;
    }
}
