using FluentValidation;
using MasterklubAPI.DTOs.Prodavci;

namespace MasterklubAPI.Validators.Prodavci;

public class CreateProdavacRequestValidator : AbstractValidator<CreateProdavacRequest>
{
    public CreateProdavacRequestValidator()
    {
        RuleFor(x => x.Ime)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Prezime)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(100);

        RuleFor(x => x.Lozinka)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(100);

        RuleFor(x => x.TipProdavca)
            .IsInEnum();
    }
}
