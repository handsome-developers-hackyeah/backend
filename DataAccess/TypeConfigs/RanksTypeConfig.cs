using Comptee.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comptee.DataAccess.TypeConfigs;


internal sealed class RanksTypeConfig : IEntityTypeConfiguration<Ranks>
{
    public void Configure(EntityTypeBuilder<Ranks> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Number);

        builder.HasMany(c => c.Users)
            .WithOne(c => c.Rank)
            .HasForeignKey(c => c.RankId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
