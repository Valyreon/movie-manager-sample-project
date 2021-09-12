namespace ServiceLayer.Responses.JsonModels
{
    public class ReviewData
    {
        public byte Rating { get; set; }
        public string Text { get; set; }
        public string RatedBy { get; set; }
        public string RatedWhen { get; set; }
    }
}
