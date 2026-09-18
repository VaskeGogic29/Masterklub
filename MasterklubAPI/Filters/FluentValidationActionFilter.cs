using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MasterklubAPI.Filters;

public class FluentValidationActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null)
                continue;

            var tipValidatora = typeof(IValidator<>).MakeGenericType(argument.GetType());

            if (context.HttpContext.RequestServices.GetService(tipValidatora) is not IValidator validator)
                continue;

            var kontekstValidacije = new ValidationContext<object>(argument);
            var rezultat = await validator.ValidateAsync(kontekstValidacije);

            if (!rezultat.IsValid)
            {
                foreach (var greska in rezultat.Errors)
                {
                    context.ModelState.AddModelError(greska.PropertyName, greska.ErrorMessage);
                }
            }
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState));
            return;
        }

        await next();
    }
}
