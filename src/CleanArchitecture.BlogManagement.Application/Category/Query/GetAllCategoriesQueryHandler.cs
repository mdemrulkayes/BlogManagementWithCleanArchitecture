using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Category.Query;
internal sealed class GetAllCategoriesQueryHandler(IRepository repository, IMapper mapper) : IQueryHandler<GetAllCategoriesQuery, Result<List<CategoryResponse>>>
{
    public async Task<Result<List<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.FindAllAsync<Core.Category.Category>(cancellationToken);
        return mapper.Map<List<CategoryResponse>>(result);
    }
}
