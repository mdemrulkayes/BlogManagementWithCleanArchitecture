using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Tag.Create;

public sealed record CreateTagCommand(string Name, string Description) : ICommand<Result<TagResponse>>;
