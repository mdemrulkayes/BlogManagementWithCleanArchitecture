using CleanArchitecture.BlogManagement.Core.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.BlogManagement.Infrastructure.Configurations
{
    internal sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable($"{nameof(Comment)}");

            builder
                .HasKey(x => x.CommentId);
            builder
                .Property(x => x.CommentId)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.PostId)
                .IsRequired();
            builder
                .Property(x => x.Text)
                .HasMaxLength(300)
                .IsRequired();
            builder
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
