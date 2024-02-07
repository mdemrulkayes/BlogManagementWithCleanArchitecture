using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Tag.Delete;
public sealed record DeleteTagCommand(long TagId) : ICommand<Result<bool>>;
