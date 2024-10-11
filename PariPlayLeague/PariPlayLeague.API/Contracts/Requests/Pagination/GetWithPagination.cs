namespace PariPlayLeague.API.Contracts.Requests.Pagination
{
    public record GetWithPagination
    {
        public int Page { get; init; }
        public int PerPage { get; init; }
    }
}
