using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post.Query;
internal sealed class GetPostByIdQueryHandler(IRepository repository, IMapper mapper) : IQueryHandler<GetPostByIdQuery, Result<PostResponse>>
{
    public async Task<Result<PostResponse>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var postDetails = await repository.FirstOrDefaultAsync<Core.PostAggregate.Post>(x => x.PostId == request.PostId, $"{nameof(Comment)}s");
        if (postDetails is null)
        {
            return PostErrors.NotFound;
        }

        return mapper.Map<PostResponse>(postDetails);
    }
}
