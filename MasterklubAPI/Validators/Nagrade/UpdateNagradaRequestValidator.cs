using FluentValidation;
using MasterklubAPI.DTOs.Nagrade;

namespace MasterklubAPI.Validators.Nagrade;

public class UpdateNagradaRequestValidator : AbstractValidator<UpdateNagradaRequest>
{
    public UpdateNagradaRequestValidator()
    {
        RuleFor(x => x.Naziv)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.BrojPoena)
            .GreaterThan(0);

        RuleFor(x => x.Nivo)
            .InclusiveBetween(1, 5);

        RuleFor(x => x.Status)
            .IsInEnum();
    }
}
