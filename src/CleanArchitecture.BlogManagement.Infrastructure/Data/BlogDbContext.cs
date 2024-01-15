using System.Reflection;
using CleanArchitecture.BlogManagement.Core.PostAggregate;
using CleanArchitecture.BlogManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.BlogManagement.Infrastructure.Data;

public class BlogDbContext(DbContextOptions<BlogDbContext> options) 
    : IdentityDbContext<ApplicationUser,IdentityRole<Guid>, Guid>(options)
{
    //public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
