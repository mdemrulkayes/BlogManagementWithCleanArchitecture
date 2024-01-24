using CleanArchitecture.BlogManagement.Core.Base;
using FluentValidation;
using TagCore = CleanArchitecture.BlogManagement.Core.Tag.Tag;

namespace CleanArchitecture.BlogManagement.Application.Tag.Create;
public sealed class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    private readonly IRepository _repository;
    public CreateTagCommandValidator(IRepository repository)
    {
        _repository = repository;
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name can not be empty")
            .Length(5, 50)
            .WithMessage("Name can not be less than 5 characters and can not be more than 50 characters")
            .MustAsync(async (name, token) =>
            {
                var isNameAlreadyExists = await repository.AnyAsync<TagCore>(x =>
                    x.Name.ToLower() == name.ToLower());
                return !isNameAlreadyExists;
            })
            .WithMessage("Tag name is already exists");

        RuleFor(x => x.Description)
            .Length(10, 150)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage("Description can not be less than 10 characters and can not be more than 150 characters");

    }
}
