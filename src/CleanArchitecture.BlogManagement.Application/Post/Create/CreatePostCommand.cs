using CleanArchitecture.BlogManagement.Core.PostAggregate;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.Create;
public sealed record CreatePostCommand(string Title, string Text, string Slug, PostStatus Status = PostStatus.Draft) : ICommand<Result<long>>;
