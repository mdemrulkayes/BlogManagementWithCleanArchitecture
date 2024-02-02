using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;
public sealed class GetAllTagsQuery : QueryStringParameter, IQuery<Result<PaginatedList<TagResponse>>>;
