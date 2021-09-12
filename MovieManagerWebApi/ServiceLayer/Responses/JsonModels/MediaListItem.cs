namespace ServiceLayer.Responses.JsonModels
{
    public abstract class MediaListItem
    {
        public int Id { get; set; }
        public int ReleaseYear { get; set; }
        public string Title { get; set; }
        public string CoverPath { get; set; }
        public double? AverageReview { get; set; }
    }
}
