namespace CommonItems.Models
{
    public class QueryModel
    {
        public string? SearchPhrase { get; set; }
        public string? SortBy { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
