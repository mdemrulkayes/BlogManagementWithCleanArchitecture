using SharedKernel;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate.Events;
public sealed record CommentAdded(Guid UserGuid, string CommentText) : IDomainEvent;
