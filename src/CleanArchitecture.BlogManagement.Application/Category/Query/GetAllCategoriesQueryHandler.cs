using AutoMapper;
using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Category;

namespace CleanArchitecture.BlogManagement.Application.Category.Query;
internal sealed class GetAllCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper) : IQueryHandler<GetAllCategoriesQuery, Result<PagedListDto<CategoryResponse>>>
{
    public async Task<Result<PagedListDto<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetAllCategories(request.PageSize, request.PageNumber, cancellationToken);
        return mapper.Map<PagedListDto<CategoryResponse>>(result);
    }
}
