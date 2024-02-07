using CleanArchitecture.BlogManagement.Core.PostAggregate;
using CleanArchitecture.BlogManagement.Core.Tag;
using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.AddPostTag;
internal sealed class AddPostTagCommandHandler(IPostRepository postRepository, ITagRepository tagRepository, IUnitOfWork unitOfWork) : ICommandHandler<AddPostTagCommand, Result<long>>
{
    public async Task<Result<long>> Handle(AddPostTagCommand request, CancellationToken cancellationToken)
    {
        var postDetails = await postRepository.GetPostDetailsWithTags(request.PostId, cancellationToken);
        if (postDetails is null)
        {
            return PostErrors.NotFound;
        }

        var tagDetails = await tagRepository.GetTagDetailsByText(request.Tag.Trim(), cancellationToken);
        if (tagDetails is null)
        {
            //Create the tag
            var tag = Core.Tag.Tag.Create(request.Tag);
            if (!tag.IsSuccess || tag.Value is null)
            {
                return tag.Error;
            }

            await tagRepository.Add(tag.Value);
            postDetails.AddPostTag(tag.Value);
        }
        else
        {
            postDetails.AddPostTag(tagDetails);
        }

        await postRepository.Update(postDetails);
        await unitOfWork.CommitAsync(cancellationToken);

        return postDetails.PostId;
    }
}
