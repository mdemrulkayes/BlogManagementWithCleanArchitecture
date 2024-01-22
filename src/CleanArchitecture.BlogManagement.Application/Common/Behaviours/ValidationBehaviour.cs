using CleanArchitecture.BlogManagement.Core.Base;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.BlogManagement.Application.Common.Behaviours;
public sealed class ValidationBehaviour<TRequest, TResponse>(ILogger<ValidationBehaviour<TRequest, TResponse>> logger, IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : IBaseResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any()) return await next();

        logger.LogInformation("Inside validation behaviour pipeline and found validation errors.");
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (!failures.Any()) return await next();

        var errors = failures.ConvertAll(error => Error.Validation(error.PropertyName, error.ErrorMessage));
        return (dynamic)errors;
    }
}
