using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;

public sealed record GetAllTagsQuery : QueryStringParameter, IQuery<Result<PagedListDto<TagResponse>>>;