using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;

public sealed record GetTagByIdCommand(long TagId) : ICommand<Result<TagResponse>>;
