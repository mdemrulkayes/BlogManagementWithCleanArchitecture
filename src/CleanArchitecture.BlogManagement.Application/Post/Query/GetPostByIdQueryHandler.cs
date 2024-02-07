using AutoMapper;
using CleanArchitecture.BlogManagement.Core.PostAggregate;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.Query;
internal sealed class GetPostByIdQueryHandler(IPostRepository repository, IMapper mapper) : IQueryHandler<GetPostByIdQuery, Result<PostResponse>>
{
    public async Task<Result<PostResponse>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var postDetails = await repository.GetPostDetailsById(request.PostId, cancellationToken);
        if (postDetails is null)
        {
            return PostErrors.NotFound;
        }

        return mapper.Map<PostResponse>(postDetails);
    }
}
