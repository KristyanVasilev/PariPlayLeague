using PariPlayLeague.Domain.Abstractions;

namespace PariPlayLeague.Domain.Entities
{
    public class Match : Entity<Guid>
    {
        public DateTime Date { get; set; }
        public int GoalsHomeTeam { get; set; }
        public int GoalsAwayTeam { get; set; }

        public Guid HomeTeamId { get; set; } = default!;
        public Team HomeTeam { get; set; } = default!;
        public Guid AwayTeamId { get; set; } = default!;
        public Team AwayTeam { get; set; } = default!;
    }
}
