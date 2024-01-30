using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Tag;

namespace CleanArchitecture.BlogManagement.Application.Tag.Delete;
internal sealed class DeleteTagCommandHandler(ITagRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteTagCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteTagCommand request, CancellationToken cancellationToken = default)
    {
        var tag = await repository.FirstOrDefaultAsync(x => x.TagId == request.TagId);

        if (tag == null)
        {
            return TagErrors.TagNotFound;
        }

        await repository.Delete(tag);
        await unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}
