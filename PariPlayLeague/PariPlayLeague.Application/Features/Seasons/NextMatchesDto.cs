using PariPlayLeague.Domain.Entities;

namespace PariPlayLeague.Application.Features.Seasons
{
    public class NextMatchesDto
    {
        public int NextMatchesCount { get; set; }
        public List<Match> Matches { get; set; } = new List<Match>();
    }
}
