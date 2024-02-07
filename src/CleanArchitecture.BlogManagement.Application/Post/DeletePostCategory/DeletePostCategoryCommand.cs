using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.DeletePostCategory;
public sealed record DeletePostCategoryCommand(long PostId, long CategoryId) : ICommand<Result<long>>;
