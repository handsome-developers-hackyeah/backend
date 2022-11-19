using Comptee.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comptee.DataAccess.TypeConfigs;

internal sealed class ReportedPostTypeConfig : IEntityTypeConfiguration<ReportedPosts>
{
    public void Configure(EntityTypeBuilder<ReportedPosts> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Post)
            .WithMany(c => c.ReportedPosts)
            .HasForeignKey(c => c.PostId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Reporter)
            .WithMany(c => c.ReportedPosts)
            .HasForeignKey(c => c.ReporterId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    }
}