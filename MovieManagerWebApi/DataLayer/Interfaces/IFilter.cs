namespace DataLayer.Interfaces
{
    public interface IFilter
    {
        public string Token { get; set; }
        public string OrderBy { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
}
