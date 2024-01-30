using CleanArchitecture.BlogManagement.Core.Tag;
using FluentValidation;

namespace CleanArchitecture.BlogManagement.Application.Tag.Create;
public sealed class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    private readonly ITagRepository _repository;
    public CreateTagCommandValidator(ITagRepository repository)
    {
        _repository = repository;
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name can not be empty")
            .Length(5, 50)
            .WithMessage("Name can not be less than 5 characters and can not be more than 50 characters")
            .MustAsync(async (name, token) =>
            {
                var isNameAlreadyExists = await repository.AnyAsync(x =>
                    x.Name.ToLower() == name.ToLower());
                return !isNameAlreadyExists;
            })
            .WithMessage("Tag name already exists");

        RuleFor(x => x.Description)
            .Length(10, 150)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage("Description can not be less than 10 characters and can not be more than 150 characters");

    }
}
