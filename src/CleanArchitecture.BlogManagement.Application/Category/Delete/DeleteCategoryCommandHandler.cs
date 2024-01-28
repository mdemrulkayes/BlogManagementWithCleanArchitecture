﻿using CleanArchitecture.BlogManagement.Core.Base;
using CleanArchitecture.BlogManagement.Core.Category;

namespace CleanArchitecture.BlogManagement.Application.Category.Delete;
internal sealed class DeleteCategoryCommandHandler(IRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteCategoryCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category =
            await repository.FirstOrDefaultAsync<Core.Category.Category>(x => x.CategoryId == request.CategoryId);
        if (category == null)
        {
            return CategoryErrors.NotFound;
        }

        await repository.Delete(category);
        await unitOfWork.CommitAsync(cancellationToken);

        return true;
    }
}
