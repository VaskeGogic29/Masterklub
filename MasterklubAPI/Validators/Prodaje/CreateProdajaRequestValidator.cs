using FluentValidation;
using MasterklubAPI.DTOs.Prodaje;

namespace MasterklubAPI.Validators.Prodaje;

public class CreateProdajaRequestValidator : AbstractValidator<CreateProdajaRequest>
{
    public CreateProdajaRequestValidator()
    {
        RuleFor(x => x.ProizvodId)
            .GreaterThan(0);

        RuleFor(x => x.Kolicina)
            .GreaterThan(0);
    }
}
