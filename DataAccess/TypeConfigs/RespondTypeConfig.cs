using Comptee.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comptee.DataAccess.TypeConfigs;


internal sealed class RespondTypeConfig : IEntityTypeConfiguration<Respond>
{
    public void Configure(EntityTypeBuilder<Respond> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.BeCompteeActivity)
            .WithMany(c => c.Responds)
            .HasForeignKey(c => c.BeCompteeActivityId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(c => c.User)
            .WithMany(c => c.Responds)
            .HasForeignKey(c => c.UserId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
