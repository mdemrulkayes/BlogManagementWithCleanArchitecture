using CleanArchitecture.BlogManagement.Core.PostAggregate;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.Delete;
internal sealed class DeletePostCommandHandler(IPostRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<DeletePostCommand, Result<bool>>
{
    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<Result<bool>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var postDetails = await repository.GetPostDetailsById(request.PostId, cancellationToken);

        if (postDetails is null)
        {
            return PostErrors.NotFound;
        }

        await repository.Delete(postDetails);
        await unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}
