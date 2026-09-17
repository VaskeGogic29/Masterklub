using MasterklubAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MasterklubAPI.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case NotFoundException notFoundException:
                context.Result = new NotFoundObjectResult(new { poruka = notFoundException.Message });
                context.ExceptionHandled = true;
                break;

            case InvalidOperationException invalidOperationException:
                context.Result = new ConflictObjectResult(new { poruka = invalidOperationException.Message });
                context.ExceptionHandled = true;
                break;
        }
    }
}
