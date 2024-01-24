using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;
public sealed class GetAllTagsQuery : IQuery<Result<List<TagResponse>>>;
