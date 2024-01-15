using CleanArchitecture.BlogManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.BlogManagement.Infrastructure.Configurations;
internal sealed class UserTokensConfiguration : IEntityTypeConfiguration<IdentityUserToken<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder)
    {
        builder
            .ToTable("UserTokens")
            .HasKey(x => new
            {
                x.LoginProvider,
                x.UserId,
                x.Name
            });

        builder
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}
