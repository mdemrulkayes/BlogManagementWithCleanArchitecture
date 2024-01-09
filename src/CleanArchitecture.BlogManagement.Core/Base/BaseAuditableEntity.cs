namespace CleanArchitecture.BlogManagement.Core.Base;
public class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset CreatedDate { get; set; }
    public long CreatedBy { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
    public long? UpdatedBy { get; set; }
}
