using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.BlogManagement.Infrastructure.Configurations;
internal sealed class RoleClaimsConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
    {
        builder
            .ToTable("RoleClaims")
            .HasKey(x => x.Id);
        builder
            .HasOne<IdentityRole<Guid>>()
            .WithMany()
            .HasForeignKey(x => x.RoleId);
    }
}
