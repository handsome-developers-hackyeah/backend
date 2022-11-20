using Comptee.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comptee.DataAccess.TypeConfigs;

internal sealed class UserTypeConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.Posts)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.Responds)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);    
        
        
        builder.HasOne(c => c.Rank)
            .WithMany(c => c.Users)
            .HasForeignKey(c => c.RankId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    }
}