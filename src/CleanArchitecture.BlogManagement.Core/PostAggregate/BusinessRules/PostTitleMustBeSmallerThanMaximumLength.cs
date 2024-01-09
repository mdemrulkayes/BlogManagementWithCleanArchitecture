using CleanArchitecture.BlogManagement.Core.BusinessRuleEngine;

namespace CleanArchitecture.BlogManagement.Core.PostAggregate.BusinessRules;
internal sealed class PostTitleMustBeSmallerThanMaximumLength : IBusinessRule
{
    private readonly int _titleMaxLength;

    internal PostTitleMustBeSmallerThanMaximumLength(int length) => _titleMaxLength = length;


    public bool IsValid() => _titleMaxLength <= 100;


    public string ErrorMessage => $"Post title can not be more than 100 characters";
}
