using Comptee.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comptee.DataAccess.TypeConfigs;

internal sealed class BeCompteeTypeConfig : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.User)
            .WithMany(c => c.Posts)
            .HasForeignKey(c => c.UserId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Responds)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
                        
        builder.HasMany(c => c.ReportedPosts)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}