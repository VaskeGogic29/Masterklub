using FluentValidation;
using MasterklubAPI.DTOs.Administratori;

namespace MasterklubAPI.Validators.Administratori;

public class UpdateAdministratorRequestValidator : AbstractValidator<UpdateAdministratorRequest>
{
    public UpdateAdministratorRequestValidator()
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
