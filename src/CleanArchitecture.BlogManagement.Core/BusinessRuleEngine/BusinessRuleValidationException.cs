namespace CleanArchitecture.BlogManagement.Core.BusinessRuleEngine;
internal sealed class BusinessRuleValidationException(string errorMessage) : InvalidOperationException(errorMessage);
