using PariPlayLeague.Domain.Abstractions;
using PariPlayLeague.Domain.Abstractions.Interfaces;

namespace PariPlayLeague.Domain.Entities
{
    public class Team : Entity<Guid>, IDeletable, IDatable
    {
        public string Name { get; set; } = default!;
        public string President { get; set; } = default!;
        public int Champion { get; set; }


        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        // Extend functionality for players in the team, not included in the scope of the task
        // public List<Player> Players { get; set; } = new List<Player>();
        public List<TeamSeason> TeamSeasons { get; set; } = new List<TeamSeason>();
        public List<Match> HomeMatches { get; set; } = new List<Match>();
        public List<Match> AwayMatches { get; set; } = new List<Match>();
    }
}
