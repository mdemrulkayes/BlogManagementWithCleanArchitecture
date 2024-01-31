using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Post.AddPostTag;
public sealed record AddPostTagCommand(long PostId, string Tag) : ICommand<Result<long>>;
