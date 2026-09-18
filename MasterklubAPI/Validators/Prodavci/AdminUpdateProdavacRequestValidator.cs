using FluentValidation;
using MasterklubAPI.DTOs.Prodavci;

namespace MasterklubAPI.Validators.Prodavci;

public class AdminUpdateProdavacRequestValidator : AbstractValidator<AdminUpdateProdavacRequest>
{
    public AdminUpdateProdavacRequestValidator()
    {
        RuleFor(x => x.TipProdavca)
            .IsInEnum();

        RuleFor(x => x.Status)
            .IsInEnum();
    }
}
