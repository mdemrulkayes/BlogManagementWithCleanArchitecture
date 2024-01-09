namespace CleanArchitecture.BlogManagement.Core.BusinessRuleEngine;
internal interface IBusinessRule
{
    bool IsValid();

    string ErrorMessage { get; }
}
