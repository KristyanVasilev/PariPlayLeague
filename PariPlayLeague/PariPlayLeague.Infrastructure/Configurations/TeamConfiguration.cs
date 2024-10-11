using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PariPlayLeague.Domain.Entities;

namespace PariPlayLeague.Infrastructure.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(40);

            builder.Property(t => t.President)
                   .IsRequired()
                   .HasMaxLength(45);

            builder.Property(t => t.Champion)
                   .HasDefaultValue<int>(0);
        }
    }
}
