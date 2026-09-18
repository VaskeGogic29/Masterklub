using FluentValidation;
using MasterklubAPI.DTOs.Nagrade;

namespace MasterklubAPI.Validators.Nagrade;

public class CreateNagradaRequestValidator : AbstractValidator<CreateNagradaRequest>
{
    public CreateNagradaRequestValidator()
    {
        RuleFor(x => x.Naziv)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.BrojPoena)
            .GreaterThan(0);

        RuleFor(x => x.Nivo)
            .InclusiveBetween(1, 5);
    }
}
