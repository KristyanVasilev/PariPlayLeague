using PariPlayLeague.Domain.Entities;
using PariPlayLeague.Application.Filters.Pagination;

namespace PariPlayLeague.Application.Filters
{
    public class GetAllTeamsFilter : PaginationFilter
    {
        public string? Search { get; set; }

        public override IQueryable<TEntity> ApplyFilterTo<TEntity>(IQueryable<TEntity> query)
        {
            IQueryable<Team> result = query.Cast<Team>();

            if (Search is not null)
            {
                result = result.Where(t => (t.Name).Contains(Search));
            }

            return base.ApplyFilterTo(result.Cast<TEntity>());
        }
    }
}
