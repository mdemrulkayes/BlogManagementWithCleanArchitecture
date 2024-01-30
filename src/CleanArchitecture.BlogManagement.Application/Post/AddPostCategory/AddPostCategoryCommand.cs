using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Post.AddPostCategory;
public sealed record AddPostCategoryCommand(long PostId, long[] CategoryIds) : ICommand<Result<long>>;
