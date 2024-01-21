using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Common.Tag.Create;
internal sealed class TagCreateCommandValidator : AbstractValidator<TagCreateCommand>
{
    public TagCreateCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name can not be empty")
            .Length(5, 50)
            .WithMessage("Name can not be less than 5 characters and can not be more than 50 characters");

        RuleFor(x => x.Description)
            .Length(10, 150)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage("Name can not be less than 10 characters and can not be more than 150 characters");

    }
}
