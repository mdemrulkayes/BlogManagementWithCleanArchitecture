using CleanArchitecture.BlogManagement.Core.PostAggregate;
using CleanArchitecture.BlogManagement.Infrastructure.Data;

namespace CleanArchitecture.BlogManagement.Infrastructure.Persistence.Post;
internal sealed class CommentRepository(BlogDbContext dbContext) : Repository<Comment>(dbContext), ICommentRepository;
