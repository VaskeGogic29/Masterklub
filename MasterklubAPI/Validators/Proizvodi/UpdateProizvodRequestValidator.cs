using FluentValidation;
using MasterklubAPI.DTOs.Proizvodi;

namespace MasterklubAPI.Validators.Proizvodi;

public class UpdateProizvodRequestValidator : AbstractValidator<UpdateProizvodRequest>
{
    public UpdateProizvodRequestValidator()
    {
        RuleFor(x => x.Naziv)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.BrojBodova)
            .GreaterThan(0);

        RuleFor(x => x.Kategorija)
            .IsInEnum();

        RuleFor(x => x.Status)
            .IsInEnum();
    }
}
