using AutoMapper;
using CleanArchitecture.BlogManagement.Application.Common.Mapping;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post.Query;
internal sealed class GetAllPostQueryHandler(IPostRepository repository, IMapper mapper) : IQueryHandler<GetAllPostQuery, Result<PagedListDto<PostResponse>>>
{
    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<Result<PagedListDto<PostResponse>>> Handle(GetAllPostQuery request, CancellationToken cancellationToken)
    {
        var allPosts = await repository
            .GetAllPosts(request.PageNumber, request.PageSize, cancellationToken);

        return mapper.Map<PagedListDto<PostResponse>>(allPosts);
    }
}
