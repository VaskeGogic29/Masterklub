using FluentValidation;
using MasterklubAPI.DTOs.Auth;

namespace MasterklubAPI.Validators.Auth;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Lozinka)
            .NotEmpty();
    }
}
