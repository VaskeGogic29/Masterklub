using FluentValidation;
using MasterklubAPI.DTOs.Proizvodi;

namespace MasterklubAPI.Validators.Proizvodi;

public class CreateProizvodRequestValidator : AbstractValidator<CreateProizvodRequest>
{
    public CreateProizvodRequestValidator()
    {
        RuleFor(x => x.Naziv)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.BrojBodova)
            .GreaterThan(0);

        RuleFor(x => x.Kategorija)
            .IsInEnum();
    }
}
