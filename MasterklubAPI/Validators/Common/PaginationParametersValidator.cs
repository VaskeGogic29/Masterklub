using FluentValidation;
using MasterklubAPI.Common;

namespace MasterklubAPI.Validators.Common;

public class PaginationParametersValidator : AbstractValidator<PaginationParameters>
{
    public PaginationParametersValidator()
    {
        RuleFor(x => x.BrojStranice)
            .GreaterThanOrEqualTo(1)
            .WithMessage("BrojStranice mora biti veći ili jednak 1.");

        RuleFor(x => x.VelicinaStranice)
            .InclusiveBetween(1, 100)
            .WithMessage("VelicinaStranice mora biti između 1 i 100.");
    }
}
