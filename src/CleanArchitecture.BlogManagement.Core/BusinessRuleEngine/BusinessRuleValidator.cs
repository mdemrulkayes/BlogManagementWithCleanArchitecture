namespace CleanArchitecture.BlogManagement.Core.BusinessRuleEngine;
internal static class BusinessRuleValidator
{
    internal static void Validate(IBusinessRule rule)
    {
        if (!rule.IsValid())
        {
            throw new BusinessRuleValidationException(rule.ErrorMessage);
        }
    }
}
