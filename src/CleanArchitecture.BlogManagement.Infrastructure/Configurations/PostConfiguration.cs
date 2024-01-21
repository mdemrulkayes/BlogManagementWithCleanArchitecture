using CleanArchitecture.BlogManagement.Core.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.BlogManagement.Infrastructure.Configurations;

internal sealed class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Post");

        builder
            .HasKey(x => x.PostId);
        builder
            .Property(x => x.PostId)
            .ValueGeneratedOnAdd();

        builder
            .HasMany(x => x.Comments)
            .WithOne()
            .HasForeignKey(x => x.PostId);


        builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Slug).HasMaxLength(50).IsRequired();
    }
}