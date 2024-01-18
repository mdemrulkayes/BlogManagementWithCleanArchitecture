namespace CleanArchitecture.BlogManagement.Core.Base;
public class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset CreatedDate { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
    public Guid? UpdatedBy { get; set; }
}
