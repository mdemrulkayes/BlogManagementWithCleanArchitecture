using CleanArchitecture.BlogManagement.Core.PostAggregate;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.UpdatePostStatus;
internal sealed class UpdatePostStatusCommandHandler(IPostRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<UpdatePostStatusCommand, Result<long>>
{
    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<Result<long>> Handle(UpdatePostStatusCommand request, CancellationToken cancellationToken)
    {
        var postDetails = await repository.GetPostDetailsWithoutComments(request.PostId, cancellationToken);

        if (postDetails is null)
        {
            return PostErrors.NotFound;
        }

        postDetails.SetStatus(request.Status);

        await repository.Update(postDetails);
        await unitOfWork.CommitAsync(cancellationToken);

        return postDetails.PostId;
    }
}
