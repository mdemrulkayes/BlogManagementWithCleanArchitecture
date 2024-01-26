using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.PostAggregate;

namespace CleanArchitecture.BlogManagement.Application.Post.Create;
internal sealed class CreatePostCommandHandler(IRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : ICommandHandler<CreatePostCommand, Result<PostResponse>>
{
    public async Task<Result<PostResponse>> Handle(CreatePostCommand request, CancellationToken cancellationToken = default)
    {
        var post = Core.PostAggregate.Post.CreatePost(request.Title, request.Slug, request.Text);
        if (!post.IsSuccess || post.Value is null)
        {
            return PostErrors.PostCanNotCreated;
        }

        switch (request.Status)
        {
            case PostStatus.Published:
                post.Value.MarkPostAsPublished();
                break;
            case PostStatus.Abandoned:
                post.Value.MarkPostAsAbandoned();
                break;
            case PostStatus.Draft:
            default:
                post.Value.MarkPostAsDraft();
                break;
        }

        await repository.Add(post.Value);
        await unitOfWork.CommitAsync(cancellationToken);

        return mapper.Map<PostResponse>(post.Value);
    }
}
