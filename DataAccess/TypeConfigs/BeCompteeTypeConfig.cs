using Comptee.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comptee.DataAccess.TypeConfigs;

internal sealed class BeCompteeTypeConfig : IEntityTypeConfiguration<BeCompteeActivity>
{
    public void Configure(EntityTypeBuilder<BeCompteeActivity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.User)
            .WithMany(c => c.BeCompteeActivities)
            .HasForeignKey(c => c.UserId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Responds)
            .WithOne(c => c.BeCompteeActivity)
            .HasForeignKey(c => c.BeCompteeActivityId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    }
}