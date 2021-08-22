namespace ServiceLayer.Responses
{
    public class MoviePageResponse : MovieListResponse
    {
        public int PageNumber { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
