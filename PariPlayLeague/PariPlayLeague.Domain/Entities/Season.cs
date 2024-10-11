using PariPlayLeague.Domain.Abstractions;
using PariPlayLeague.Domain.Abstractions.Interfaces;

namespace PariPlayLeague.Domain.Entities
{
    public class Season : Entity<Guid>, IDeletable
    {
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<TeamSeason> TeamSeasons { get; set; } = new List<TeamSeason>();
        public List<Match> Matches { get; set; } = new List<Match>();
    }
}
