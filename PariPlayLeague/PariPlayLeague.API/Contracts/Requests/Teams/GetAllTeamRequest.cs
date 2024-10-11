using PariPlayLeague.API.Contracts.Requests.Pagination;
using PariPlayLeague.Application.Features.Teams.Queries;
using PariPlayLeague.Application.Filters;

namespace PariPlayLeague.API.Contracts.Requests.Teams
{
    public record GetAllTeamRequest : GetWithPagination
    {
        public string? Search { get; init; }
        public static GetAllTeamQuery MapToQuery(GetAllTeamsFilter request)
        {
            return new GetAllTeamQuery
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
