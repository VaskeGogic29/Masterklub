using FluentValidation;
using MasterklubAPI.DTOs.Prodavci;

namespace MasterklubAPI.Validators.Prodavci;

public class UpdateProdavacRequestValidator : AbstractValidator<UpdateProdavacRequest>
{
    public UpdateProdavacRequestValidator()
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
    }
}
