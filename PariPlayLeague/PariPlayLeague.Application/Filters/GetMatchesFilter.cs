using Microsoft.EntityFrameworkCore;
using PariPlayLeague.Application.Filters.Pagination;
using PariPlayLeague.Domain.Entities;

namespace PariPlayLeague.Application.Filters
{
    public class GetMatchesFilter : PaginationFilter
    {
        public string? Search { get; set; }

        public override IQueryable<TEntity> ApplyFilterTo<TEntity>(IQueryable<TEntity> query)
        {
            IQueryable<Match> result = query.Cast<Match>()
                                            .Include(m => m.HomeTeam)
                                            .Include(m => m.AwayTeam);

            if (Search is not null)
            {
                result = result.Where(m => (m.HomeTeam.Name).Contains(Search) || (m.AwayTeam.Name).Contains(Search));
            }

            return base.ApplyFilterTo(result.Cast<TEntity>());
        }
    }
}
