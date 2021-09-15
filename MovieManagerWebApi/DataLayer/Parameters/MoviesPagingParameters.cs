using DataLayer.Enums;

namespace DataLayer.Parameters
{
    public class MoviesPagingParameters : PagingParameters
    {
        public MoviesOrderBy OrderBy { get; set; } = MoviesOrderBy.Rating;
    }
}
