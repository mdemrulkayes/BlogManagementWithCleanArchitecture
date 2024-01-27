using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post.Query;
internal sealed class GetAllPostQueryHandler(IPostRepository repository, IMapper mapper) : IQueryHandler<GetAllPostQuery, Result<List<PostResponse>>>
{
    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<Result<List<PostResponse>>> Handle(GetAllPostQuery request, CancellationToken cancellationToken)
    {
        var allPosts = await repository
            .FindAsync<Core.PostAggregate.Post>(
                x => x.Status == PostStatus.Published,

            y => y.OrderByDescending(z => z.CreatedDate),
                cancellationToken);

        return mapper.Map<List<PostResponse>>(allPosts);
    }
}
