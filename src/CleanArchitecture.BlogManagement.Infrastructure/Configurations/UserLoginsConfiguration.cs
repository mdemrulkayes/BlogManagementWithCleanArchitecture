using CleanArchitecture.BlogManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.BlogManagement.Infrastructure.Configurations;
internal sealed class UserLoginsConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
    {
        builder.ToTable("UserLogins").HasKey(x => new
        {
            x.LoginProvider,
            x.ProviderKey
        });

        builder
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}
