﻿using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Tag.Delete;
internal sealed class DeleteTagCommandHandler(IRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteTagCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteTagCommand request, CancellationToken cancellationToken = default)
    {
        var tag = await repository.FirstOrDefaultAsync<Core.Tag.Tag>(x => x.TagId == request.TagId);

        if (tag == null)
        {
            return TagErrors.TagNotFound;
        }

        await repository.Delete(tag);
        await unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}
