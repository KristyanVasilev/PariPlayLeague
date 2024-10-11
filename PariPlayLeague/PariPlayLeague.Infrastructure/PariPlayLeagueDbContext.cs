using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PariPlayLeague.Domain.Entities;
using PariPlayLeague.Infrastructure.Extensions;

namespace PariPlayLeague.Infrastructure
{
    public class PariPlayLeagueDbContext : IdentityDbContext<IdentityUser>
    {
        public PariPlayLeagueDbContext(DbContextOptions<PariPlayLeagueDbContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamSeason> TeamsSeasons { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<TeamStatistic> TeamsStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TeamSeason>()
                .HasOne(ts => ts.Team)
                .WithMany(t => t.TeamSeasons)
                .HasForeignKey(ts => ts.TeamId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TeamSeason>()
                .HasOne(ts => ts.Season)
                .WithMany(s => s.TeamSeasons)
                .HasForeignKey(ts => ts.SeasonId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TeamSeason>()
                .OwnsOne(ts => ts.TeamStatistic);

            modelBuilder.Entity<Match>().ToTable(t => t.HasCheckConstraint("CK_GoalsHomeTeam_Range", "[GoalsHomeTeam] >= 0"));
            modelBuilder.Entity<Match>().ToTable(t => t.HasCheckConstraint("CK_GoalsAwayTeam_Range", "[GoalsAwayTeam] >= 0"));
            modelBuilder.Entity<Team>().ToTable(t => t.HasCheckConstraint("CK_Champion_Range", "[Champion] >= 0"));
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.UseSoftDelete();
            this.UseTrackingOnCreate();

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
