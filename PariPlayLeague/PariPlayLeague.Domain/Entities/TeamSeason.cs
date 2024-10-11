using PariPlayLeague.Domain.Abstractions;

namespace PariPlayLeague.Domain.Entities
{
    public class TeamSeason : Entity<Guid>
    {
        public Guid TeamId { get; set; }
        public Team Team { get; set; } = default!;

        public Guid SeasonId { get; set; }
        public Season Season { get; set; } = default!;

        public TeamStatistic TeamStatistic { get; set; } = default!;
    }
}
