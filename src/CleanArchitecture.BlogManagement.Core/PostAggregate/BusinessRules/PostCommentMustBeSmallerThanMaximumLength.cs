using CleanArchitecture.BlogManagement.Core.BusinessRuleEngine;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate.BusinessRules;
internal sealed class PostCommentMustBeSmallerThanMaximumLength : IBusinessRule
{
    private readonly int _maxAllowedLength;
    internal PostCommentMustBeSmallerThanMaximumLength(int length) => _maxAllowedLength = length;
    public bool IsValid() => _maxAllowedLength <= 200;


    public string ErrorMessage => "Comment length can not be more than 200 characters";
}
