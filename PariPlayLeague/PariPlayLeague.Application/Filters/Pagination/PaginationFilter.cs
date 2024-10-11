using AutoFilterer.Types;

namespace PariPlayLeague.Application.Filters.Pagination
{
    public class PaginationFilter : PaginationFilterBase
    {
        public override int PerPage
        {
            get => base.PerPage > 50 ? 50 : base.PerPage;
            set => base.PerPage = value < 1 ? 10 : value;
        }

        public override int Page
        {
            get => base.Page;
            set => base.Page = value < 1 ? 1 : value;
        }
    }
}
