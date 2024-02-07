using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.AddPostCategory;
public sealed record AddPostCategoryCommand(long PostId, long CategoryId) : ICommand<Result<long>>;
