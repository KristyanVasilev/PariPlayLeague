using PariPlayLeague.Domain.Entities;

namespace PariPlayLeague.Application.Features.Seasons
{
    public class PastMatchesDto
    {
        public int PastMatchesCount { get; set; }
        public List<Match> Matches { get; set; } = new List<Match>();
    }
}
