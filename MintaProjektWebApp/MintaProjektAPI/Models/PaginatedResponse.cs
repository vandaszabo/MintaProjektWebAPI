namespace MintaProjektAPI.Models
{
    public class PaginatedResponse<T>
    {
        public int TotalRecords { get; set; }
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? TotalPages { get; set; }
    }
}
