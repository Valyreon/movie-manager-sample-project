namespace ServiceLayer.Responses
{
    public class TVShowDetailsResponse : MediaDetailsResponse
    {
        public int EndYear { get; set; }
        public int NumberOfSeasons { get; set; }
    }
}
