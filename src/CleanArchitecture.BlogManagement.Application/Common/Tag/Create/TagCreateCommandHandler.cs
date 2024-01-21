using AutoMapper;
using CleanArchitecture.BlogManagement.Core.Base;
using TagCore = CleanArchitecture.BlogManagement.Core.Tag.Tag;

namespace CleanArchitecture.BlogManagement.Application.Common.Tag.Create;
internal sealed class TagCreateCommandHandler(IRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : ICommandHandler<TagCreateCommand, Result<TagResponse>>
{
    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<Result<TagResponse>> Handle(TagCreateCommand request, CancellationToken cancellationToken)
    {
        var tag = TagCore.Create(request.Name, request.Description);

        await repository.Add(tag);
        await unitOfWork.CommitAsync(cancellationToken);

        return mapper.Map<TagCore, TagResponse>(tag);
    }
}
