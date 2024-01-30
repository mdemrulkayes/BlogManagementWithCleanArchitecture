using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Category;

namespace CleanArchitecture.BlogManagement.Application.Category.Query;
internal sealed class GetAllCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper) : IQueryHandler<GetAllCategoriesQuery, Result<List<CategoryResponse>>>
{
    public async Task<Result<List<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetAllCategories(cancellationToken);
        return mapper.Map<List<CategoryResponse>>(result);
    }
}
