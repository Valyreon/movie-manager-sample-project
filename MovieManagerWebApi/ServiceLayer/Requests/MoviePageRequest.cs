using DataLayer.Enums;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceLayer.Requests
{
    public class MoviePageRequest
    {
        [RegularExpression("^[\\ a-zA-Z0-9]{2,}$")]
        public string Token { get; set; }
        [Range(0, int.MaxValue)]
        public int PageNumber { get; set; } = 0;
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;
        [EnumDataType(typeof(MoviesOrderBy))]
        [JsonConverter(typeof(StringEnumConverter))]
        public MoviesOrderBy OrderBy { get; set; } = MoviesOrderBy.Rating;
        [EnumDataType(typeof(OrderDirection))]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderDirection OrderDirection { get; set; } = OrderDirection.Descending;
    }
}
