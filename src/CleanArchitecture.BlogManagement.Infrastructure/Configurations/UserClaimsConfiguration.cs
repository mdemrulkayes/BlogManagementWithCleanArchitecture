using CleanArchitecture.BlogManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.BlogManagement.Infrastructure.Configurations;
internal sealed class UserClaimsConfiguration : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
    {
        builder
            .ToTable("UserClaims")
            .HasKey(x => x.Id);

        builder
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}
