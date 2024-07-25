namespace MintaProjektAPI.Models
{
    public class PaginatedResponse<T>
    {
        public int TotalRecords { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
