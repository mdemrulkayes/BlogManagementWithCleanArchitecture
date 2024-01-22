using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;
public sealed class GetTagsCommand : IQuery<Result<List<TagResponse>>>;
