using CleanArchitecture.BlogManagement.Core.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.BlogManagement.Infrastructure.Configurations;
internal sealed class PostCategoryConfiguration : IEntityTypeConfiguration<PostCategory>
{
    /// <summary>
    /// Implement Post Category Configuration
    /// </summary>
    /// <param name="builder"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Configure(EntityTypeBuilder<PostCategory> builder)
    {
        builder.ToTable($"{nameof(PostCategory)}");

        builder
            .HasKey(x => new { x.PostId, x.CategoryId });

        builder
            .Property(x => x.PostCategoryId)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Post)
            .WithMany(x => x.PostCategories)
            .HasForeignKey(x => x.PostId);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.PostCategories)
            .HasForeignKey(x => x.CategoryId);
    }
}
