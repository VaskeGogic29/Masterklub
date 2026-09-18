using FluentValidation;
using MasterklubAPI.DTOs.NarudzbineNagrada;

namespace MasterklubAPI.Validators.NarudzbineNagrada;

public class CreateNarudzbinaNagradeRequestValidator : AbstractValidator<CreateNarudzbinaNagradeRequest>
{
    public CreateNarudzbinaNagradeRequestValidator()
    {
        RuleFor(x => x.NagradaId)
            .GreaterThan(0);
    }
}
