using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Common.Tag.Create;
public sealed class TagCreateCommand : ICommand<Result<TagResponse>>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
