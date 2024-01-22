using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Tag.Create;
public sealed class CreateTagCommand : ICommand<Result<TagResponse>>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
