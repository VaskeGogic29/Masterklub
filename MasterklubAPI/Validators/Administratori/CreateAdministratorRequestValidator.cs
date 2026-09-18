using FluentValidation;
using MasterklubAPI.DTOs.Administratori;

namespace MasterklubAPI.Validators.Administratori;

public class CreateAdministratorRequestValidator : AbstractValidator<CreateAdministratorRequest>
{
    public CreateAdministratorRequestValidator()
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
    }
}
