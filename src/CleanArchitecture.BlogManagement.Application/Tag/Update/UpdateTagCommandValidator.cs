using CleanArchitecture.BlogManagement.Core.Base;
using FluentValidation;
using TagCore = CleanArchitecture.BlogManagement.Core.Tag.Tag;

namespace CleanArchitecture.BlogManagement.Application.Tag.Update;
internal sealed class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
{
    private readonly IRepository _repository;
    public UpdateTagCommandValidator(IRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.TagId)
            .NotNull()
            .WithMessage("Tag Id can not null")
            .GreaterThan(0)
            .WithMessage("Tag value can not be 0 or less than 0");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name can not be empty")
            .Length(5, 50)
            .WithMessage("Name can not be less than 5 characters and can not be more than 50 characters")
            .MustAsync(async (command, name, token) =>
            {
                var isNameAlreadyExists = await repository.AnyAsync<TagCore>(x =>
                    x.Name.ToLower() == name.ToLower() && x.TagId != command.TagId);
                return !isNameAlreadyExists;
            })
            .WithMessage("Tag name is already exists");

        RuleFor(x => x.Description)
            .Length(10, 150)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage("Description can not be less than 10 characters and can not be more than 150 characters");
    }
}
