namespace MintaProjektAPI.Models
{
    public record PaginationRequest(
        int CurrentPage, 
        int PageSize, 
        string? OrderByColumn,
        string? OrderDir, 
        Dictionary<string, string>? Filters
        );
}
