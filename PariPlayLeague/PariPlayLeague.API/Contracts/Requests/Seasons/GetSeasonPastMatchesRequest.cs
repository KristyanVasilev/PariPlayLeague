using PariPlayLeague.API.Contracts.Requests.Pagination;
using PariPlayLeague.Application.Features.Seasons.Queries;
using PariPlayLeague.Application.Filters;

namespace PariPlayLeague.API.Contracts.Requests.Seasons
{
    public record GetSeasonPastMatchesRequest : GetWithPagination
    {
        public string? Search { get; init; }
        public static GetPastMatchesQuery MapToQuery(GetMatchesFilter request)
        {
            return new GetPastMatchesQuery
            {
                Filter = new()
                {
                    Search = request.Search,
                    Page = request.Page,
                    PerPage = request.PerPage,
                }
            };
        }
    }
}
