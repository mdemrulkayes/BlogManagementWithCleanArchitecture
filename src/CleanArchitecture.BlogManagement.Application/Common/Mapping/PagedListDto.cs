namespace CleanArchitecture.BlogManagement.Application.Common.Mapping;
public sealed class PagedListDto<T>
{
    public int TotalCount { get; set; }
    public IReadOnlyCollection<T>? Items { get; set; }
}
